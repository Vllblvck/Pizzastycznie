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
                sqlCmd.CommandText = DbProcedures.InsertOrder.ProcedureName;
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.UserId,
                        DbType = DbType.Int64,
                        Value = order.UserId
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.OrderComments,
                        DbType = DbType.String,
                        Value = order.Comments
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.StatusDate,
                        DbType = DbType.DateTime,
                        Value = order.StatusDate
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.OrderStatus,
                        DbType = DbType.String,
                        Value = order.Status
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.CustomerPhone,
                        DbType = DbType.String,
                        Value = order.CustomerPhone
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.DeliveryAddress,
                        DbType = DbType.String,
                        Value = order.DeliveryAddress
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.PaymentMethod,
                        DbType = DbType.Int32,
                        Value = order.PaymentMethod
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.TotalPrice,
                        DbType = DbType.Decimal,
                        Value = order.TotalPrice
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertOrder.Parameters.SelfPickup,
                        DbType = DbType.Boolean,
                        Value = order.SelfPickup
                    }
                });

                _logger.LogInformation("Inserting order into database");
                await sqlCmd.ExecuteNonQueryAsync();

                _logger.LogInformation("Selecting last insert id");
                long orderId = 0;
                sqlCmd.CommandText = DbProcedures.SelectLastInsertId.ProcedureName;
                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        orderId = sqlReader.GetInt64(DbTables.LastInsertId);
                    }
                }
                // For some reason this doesn't work :(
                // var orderId = sqlCmd.LastInsertedId;

                _logger.LogInformation("Preparing sql command to insert order food");
                sqlCmd.CommandText = DbProcedures.InsertOrderFood.ProcedureName;
                foreach (var food in order.OrderFood)
                {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddRange(new[]
                    {
                        new MySqlParameter
                        {
                            ParameterName = DbProcedures.InsertOrderFood.Parameters.OrderId,
                            DbType = DbType.Int64,
                            Value = orderId
                        },
                        new MySqlParameter
                        {
                            ParameterName = DbProcedures.InsertOrderFood.Parameters.FoodName,
                            DbType = DbType.String,
                            Value = food.Name
                        },
                        new MySqlParameter
                        {
                            ParameterName = DbProcedures.InsertOrderFood.Parameters.Amount,
                            DbType = DbType.Int32,
                            Value = food.Amount
                        },
                    });
                    _logger.LogInformation("Inserting order food");
                    await sqlCmd.ExecuteNonQueryAsync();
                }

                _logger.LogInformation("Preparing sql command to insert order additives");
                sqlCmd.CommandText = DbProcedures.InsertOrderAdditive.ProcedureName;
                foreach (var additive in order.OrderAdditives)
                {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddRange(new[]
                    {
                        new MySqlParameter
                        {
                            ParameterName = DbProcedures.InsertOrderAdditive.Parameters.OrderId,
                            DbType = DbType.Int64,
                            Value = orderId
                        },
                        new MySqlParameter
                        {
                            ParameterName = DbProcedures.InsertOrderAdditive.Parameters.AdditiveName,
                            DbType = DbType.String,
                            Value = additive.Name
                        },
                        new MySqlParameter
                        {
                            ParameterName = DbProcedures.InsertOrderAdditive.Parameters.Amount,
                            DbType = DbType.Int32,
                            Value = additive.Amount
                        }
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
                sqlCmd.CommandText = DbProcedures.SelectOrdersForUser.ProcedureName;
                sqlCmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = DbProcedures.SelectOrdersForUser.Parameters.Email,
                    DbType = DbType.String,
                    Value = email
                });

                _logger.LogInformation("Selecting orders");
                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        var commentsOrdinal = sqlReader.GetOrdinal(DbTables.Orders.OrderComments);

                        result.Add(new Order
                        {
                            Id = sqlReader.GetInt64(DbTables.Orders.OrderId),
                            StatusDate = sqlReader.GetDateTime(DbTables.Orders.StatusDate),
                            Status = sqlReader.GetString(DbTables.Orders.OrderStatus),
                            CustomerPhone = sqlReader.GetString(DbTables.Orders.CustomerPhone),
                            DeliveryAddress = sqlReader.GetString(DbTables.Orders.DeliveryAddress),
                            PaymentMethod = (PaymentMethod) sqlReader.GetInt32(DbTables.Orders.PaymentMethod),
                            TotalPrice = sqlReader.GetDecimal(DbTables.Orders.TotalPrice),
                            SelfPickup = sqlReader.GetBoolean(DbTables.Orders.SelfPickup),

                            Comments = await sqlReader.IsDBNullAsync(commentsOrdinal)
                                ? null
                                : sqlReader.GetString(DbTables.Orders.OrderComments)
                        });
                    }
                }

                foreach (var order in result)
                {
                    var orderFood = new List<OrderFood>();
                    var orderAdditives = new List<OrderAdditive>();

                    _logger.LogInformation("Preparing sql command to select order content");
                    sqlCmd.CommandText = DbProcedures.SelectOrderContent.ProcedureName;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = DbProcedures.SelectOrderContent.Parameters.OrderId,
                        DbType = DbType.Int64,
                        Value = order.Id
                    });

                    _logger.LogInformation("Selecting order content");
                    await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlReader.ReadAsync())
                        {
                            orderFood.Add(new OrderFood
                            {
                                Name = sqlReader.GetString(DbTables.OrderFood.FoodName),
                                Amount = sqlReader.GetInt32(DbTables.OrderFood.Amount)
                            });
                        }

                        await sqlReader.NextResultAsync();

                        while (await sqlReader.ReadAsync())
                        {
                            orderAdditives.Add(new OrderAdditive
                            {
                                Name = sqlReader.GetString(DbTables.OrderAdditives.AdditiveName),
                                Amount = sqlReader.GetInt32(DbTables.OrderAdditives.Amount)
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
                sqlCmd.CommandText = DbProcedures.SelectPendingOrders.ProcedureName;

                _logger.LogInformation("Selecting orders");
                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        var commentsOrdinal = sqlReader.GetOrdinal(DbTables.Orders.OrderComments);

                        result.Add(new Order
                        {
                            Id = sqlReader.GetInt64(DbTables.Orders.OrderId),
                            StatusDate = sqlReader.GetDateTime(DbTables.Orders.StatusDate),
                            Status = sqlReader.GetString(DbTables.Orders.OrderStatus),
                            CustomerPhone = sqlReader.GetString(DbTables.Orders.CustomerPhone),
                            DeliveryAddress = sqlReader.GetString(DbTables.Orders.DeliveryAddress),
                            PaymentMethod = (PaymentMethod) sqlReader.GetInt32(DbTables.Orders.PaymentMethod),
                            TotalPrice = sqlReader.GetDecimal(DbTables.Orders.TotalPrice),
                            SelfPickup = sqlReader.GetBoolean(DbTables.Orders.SelfPickup),

                            Comments = await sqlReader.IsDBNullAsync(commentsOrdinal)
                                ? null
                                : sqlReader.GetString(DbTables.Orders.OrderComments)
                        });
                    }
                }

                foreach (var order in result)
                {
                    var orderFood = new List<OrderFood>();
                    var orderAdditives = new List<OrderAdditive>();

                    _logger.LogInformation("Preparing sql command to select order content");
                    sqlCmd.CommandText = DbProcedures.SelectOrderContent.ProcedureName;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = DbProcedures.SelectOrderContent.Parameters.OrderId,
                        DbType = DbType.Int64,
                        Value = order.Id
                    });

                    _logger.LogInformation("Selecting order content");
                    await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlReader.ReadAsync())
                        {
                            orderFood.Add(new OrderFood
                            {
                                Name = sqlReader.GetString(DbTables.OrderFood.FoodName),
                                Amount = sqlReader.GetInt32(DbTables.OrderFood.Amount)
                            });
                        }

                        await sqlReader.NextResultAsync();

                        while (await sqlReader.ReadAsync())
                        {
                            orderAdditives.Add(new OrderAdditive
                            {
                                Name = sqlReader.GetString(DbTables.OrderAdditives.AdditiveName),
                                Amount = sqlReader.GetInt32(DbTables.OrderAdditives.Amount)
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
                _logger.LogError($"Exception while selecting pending orders from database: {ex.Message}");

                if (transaction != null)
                    await transaction.RollbackAsync();
            }

            return result;
        }

        public async Task<bool> UpdateOrderStatusAsync(long orderId, string status)
        {
            try
            {
                _logger.LogInformation("Opening connection with database");
                await _sqlConn.OpenAsync();

                _logger.LogInformation("Preparing sql command to update order status");
                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = DbProcedures.UpdateOrderStatus.ProcedureName;
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.UpdateOrderStatus.Parameters.OrderId,
                        DbType = DbType.Int64,
                        Value = orderId
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.UpdateOrderStatus.Parameters.OrderStatus,
                        DbType = DbType.String,
                        Value = status
                    },
                });

                _logger.LogInformation("Updating order status");
                await sqlCmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while updating order status {ex.Message}");
                return false;
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    await _sqlConn.CloseAsync();
            }
        }
    }
}