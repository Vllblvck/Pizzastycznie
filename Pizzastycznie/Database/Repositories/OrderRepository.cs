using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pizzastycznie.Database.DTO;
using Pizzastycznie.Database.DTO.Enums;
using Pizzastycznie.Database.Repositories.Interfaces;

namespace Pizzastycznie.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ILogger<IOrderRepository> _logger;
        private readonly MySqlConnection _sqlConn;

        public OrderRepository(ILogger<IOrderRepository> logger)
        {
            _logger = logger;
            _sqlConn = new MySqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

        public async Task<bool> InsertOrderAsync(Order order)
        {
            MySqlTransaction transaction = null;
            bool result;
            try
            {
                _logger.LogInformation("Opening connection with database");
                await _sqlConn.OpenAsync();

                _logger.LogInformation("Beginning transaction");
                transaction = await _sqlConn.BeginTransactionAsync();

                _logger.LogInformation("Preparing sql command to insert order");
                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "InsertOrder";
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter {ParameterName = "UserId", DbType = DbType.Int64, Value = order.UserId},
                    new MySqlParameter
                        {ParameterName = "OrderComments", DbType = DbType.String, Value = order.Comments},
                    new MySqlParameter
                        {ParameterName = "StatusDate", DbType = DbType.DateTime, Value = order.StatusDate},
                    new MySqlParameter {ParameterName = "OrderStatus", DbType = DbType.String, Value = order.Status},
                    new MySqlParameter
                        {ParameterName = "CustomerPhone", DbType = DbType.String, Value = order.CustomerPhone},
                    new MySqlParameter
                        {ParameterName = "DeliveryAddress", DbType = DbType.String, Value = order.DeliveryAddress},
                    new MySqlParameter
                        {ParameterName = "PaymentMethod", DbType = DbType.Int32, Value = order.PaymentMethod},
                    new MySqlParameter
                        {ParameterName = "TotalPrice", DbType = DbType.Decimal, Value = order.TotalPrice},
                    new MySqlParameter
                        {ParameterName = "SelfPickup", DbType = DbType.Boolean, Value = order.SelfPickup},
                });

                _logger.LogInformation("Inserting order into database");
                await sqlCmd.ExecuteNonQueryAsync();

                _logger.LogInformation("Selecting last insert id");
                long orderId = 0;
                sqlCmd.CommandText = "SelectLastInsertId";
                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        orderId = sqlReader.GetInt64("LAST_INSERT_ID()");
                    }
                }
                // For some reason this doesn't work :(
                // var orderId = sqlCmd.LastInsertedId;

                _logger.LogInformation("Preparing sql command to insert order food");
                sqlCmd.CommandText = "InsertOrderFood";
                foreach (var food in order.OrderFood)
                {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddRange(new[]
                    {
                        new MySqlParameter {ParameterName = "OrderId", DbType = DbType.Int64, Value = orderId},
                        new MySqlParameter {ParameterName = "FoodName", DbType = DbType.String, Value = food.Name},
                        new MySqlParameter {ParameterName = "Amount", DbType = DbType.Int32, Value = food.Amount},
                    });

                    _logger.LogInformation("Inserting order food");
                    await sqlCmd.ExecuteNonQueryAsync();
                }

                _logger.LogInformation("Preparing sql command to insert order additives");
                sqlCmd.CommandText = "InsertOrderAdditive";
                foreach (var additive in order.OrderAdditives)
                {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddRange(new[]
                    {
                        new MySqlParameter {ParameterName = "OrderId", DbType = DbType.Int64, Value = orderId},
                        new MySqlParameter
                            {ParameterName = "AdditiveName", DbType = DbType.String, Value = additive.Name},
                        new MySqlParameter {ParameterName = "Amount", DbType = DbType.Int32, Value = additive.Amount}
                    });

                    _logger.LogInformation("Inserting order additives");
                    await sqlCmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while inserting order into database: {ex.Message}");

                if (transaction != null)
                    await transaction.RollbackAsync();

                result = false;
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    await _sqlConn.CloseAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Order>> SelectOrdersForUserAsync(string email)
        {
            MySqlTransaction transaction = null;
            var result = new List<Order>();
            try
            {
                _logger.LogInformation("Opening connection with database");
                await _sqlConn.OpenAsync();
                transaction = await _sqlConn.BeginTransactionAsync();

                _logger.LogInformation("Preparing sql command to select orders");
                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "SelectOrdersForUser";
                sqlCmd.Parameters.Add(new MySqlParameter
                    {ParameterName = "Email", DbType = DbType.String, Value = email});

                _logger.LogInformation("Selecting orders");
                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        var commentsOrdinal = sqlReader.GetOrdinal("order_comments");

                        result.Add(new Order
                        {
                            Id = sqlReader.GetInt64("id"),
                            StatusDate = sqlReader.GetDateTime("status_date"),
                            Status = sqlReader.GetString("order_status"),
                            CustomerPhone = sqlReader.GetString("customer_phone"),
                            DeliveryAddress = sqlReader.GetString("customer_phone"),
                            PaymentMethod = (PaymentMethod) sqlReader.GetInt32("payment_method"),
                            TotalPrice = sqlReader.GetDecimal("total_price"),
                            SelfPickup = sqlReader.GetBoolean("self_pickup"),

                            Comments = await sqlReader.IsDBNullAsync(commentsOrdinal)
                                ? null
                                : sqlReader.GetString("order_comments")
                        });
                    }
                }

                foreach (var order in result)
                {
                    var orderFood = new List<OrderFood>();
                    var orderAdditives = new List<OrderAdditive>();

                    _logger.LogInformation("Preparing sql command to select order content");
                    sqlCmd.CommandText = "SelectOrderContent";
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add(new MySqlParameter
                        {ParameterName = "OrderId", DbType = DbType.Int64, Value = order.Id});

                    _logger.LogInformation("Selecting order content");
                    await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlReader.ReadAsync())
                        {
                            orderFood.Add(new OrderFood
                            {
                                Name = sqlReader.GetString("food_name"),
                                Amount = sqlReader.GetInt32("amount")
                            });
                        }

                        await sqlReader.NextResultAsync();

                        while (await sqlReader.ReadAsync())
                        {
                            orderAdditives.Add(new OrderAdditive
                            {
                                Name = sqlReader.GetString("additive_name"),
                                Amount = sqlReader.GetInt32("amount")
                            });
                        }
                    }

                    order.OrderFood = orderFood;
                    order.OrderAdditives = orderAdditives;
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while selecting orders from database: {ex.Message}");

                if (transaction != null)
                    await transaction.RollbackAsync();
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    await _sqlConn.CloseAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Order>> SelectPendingOrdersAsync()
        {
            MySqlTransaction transaction = null;
            var result = new List<Order>();
            try
            {
                _logger.LogInformation("Opening connection with database");
                await _sqlConn.OpenAsync();
                transaction = await _sqlConn.BeginTransactionAsync();

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "SelectPendingOrders";

                _logger.LogInformation("Selecting orders");
                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        var commentsOrdinal = sqlReader.GetOrdinal("order_comments");

                        result.Add(new Order
                        {
                            Id = sqlReader.GetInt64("id"),
                            StatusDate = sqlReader.GetDateTime("status_date"),
                            Status = sqlReader.GetString("order_status"),
                            CustomerPhone = sqlReader.GetString("customer_phone"),
                            DeliveryAddress = sqlReader.GetString("customer_phone"),
                            PaymentMethod = (PaymentMethod) sqlReader.GetInt32("payment_method"),
                            TotalPrice = sqlReader.GetDecimal("total_price"),
                            SelfPickup = sqlReader.GetBoolean("self_pickup"),

                            Comments = await sqlReader.IsDBNullAsync(commentsOrdinal)
                                ? null
                                : sqlReader.GetString("order_comments")
                        });
                    }
                }

                foreach (var order in result)
                {
                    var orderFood = new List<OrderFood>();
                    var orderAdditives = new List<OrderAdditive>();

                    _logger.LogInformation("Preparing sql command to select order content");
                    sqlCmd.CommandText = "SelectOrderContent";
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add(new MySqlParameter
                        {ParameterName = "OrderId", DbType = DbType.Int64, Value = order.Id});

                    _logger.LogInformation("Selecting order content");
                    await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlReader.ReadAsync())
                        {
                            orderFood.Add(new OrderFood
                            {
                                Name = sqlReader.GetString("food_name"),
                                Amount = sqlReader.GetInt32("amount")
                            });
                        }

                        await sqlReader.NextResultAsync();

                        while (await sqlReader.ReadAsync())
                        {
                            orderAdditives.Add(new OrderAdditive
                            {
                                Name = sqlReader.GetString("additive_name"),
                                Amount = sqlReader.GetInt32("amount")
                            });
                        }
                    }

                    order.OrderFood = orderFood;
                    order.OrderAdditives = orderAdditives;
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception while selecting pending orders from database: {ex.Message}");

                if (transaction != null)
                    await transaction.RollbackAsync();
            }

            return result;
        }

        public async Task<bool> UpdateOrderStatusAsync()
        {
            throw new NotImplementedException();
        }
    }
}