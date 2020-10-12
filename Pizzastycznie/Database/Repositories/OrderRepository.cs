using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pizzastycznie.Database.DTO;
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
                await _sqlConn.OpenAsync();
                transaction = await _sqlConn.BeginTransactionAsync();
                
                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "InsertOrder";
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter {ParameterName = "UserId", DbType = DbType.Int64, Value = order.UserId},
                    new MySqlParameter {ParameterName = "OrderComments", DbType = DbType.String, Value = order.Comments},
                    new MySqlParameter {ParameterName = "StatusDate", DbType = DbType.DateTime, Value = order.StatusDate},
                    new MySqlParameter {ParameterName = "OrderStatus", DbType = DbType.String, Value = order.Status},
                    new MySqlParameter {ParameterName = "CustomerPhone", DbType = DbType.String, Value = order.CustomerPhone},
                    new MySqlParameter {ParameterName = "DeliveryAddress", DbType = DbType.String, Value = order.DeliveryAddress},
                    new MySqlParameter {ParameterName = "PaymentMethod", DbType = DbType.Int32, Value = order.PaymentMethod},
                    new MySqlParameter {ParameterName = "TotalPrice", DbType = DbType.Decimal, Value = order.TotalPrice},
                    new MySqlParameter {ParameterName = "SelfPickup", DbType = DbType.Boolean, Value = order.SelfPickup},
                });

                await sqlCmd.ExecuteNonQueryAsync();

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

                sqlCmd.CommandText = "InsertOrderFood";
                foreach (var food in order.OrderFood) {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddRange(new[]
                    {
                        new MySqlParameter {ParameterName = "OrderId", DbType = DbType.Int64, Value = orderId},
                        new MySqlParameter {ParameterName = "FoodName", DbType = DbType.String, Value = food.Name},
                        new MySqlParameter {ParameterName = "Amount", DbType = DbType.Int32, Value = food.Amount},
                    });

                    await sqlCmd.ExecuteNonQueryAsync();
                }

                sqlCmd.CommandText = "InsertOrderAdditive";
                foreach (var additive in order.OrderAdditives)
                {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddRange(new[]
                    {
                        new MySqlParameter {ParameterName = "OrderId", DbType = DbType.Int64, Value = orderId},
                        new MySqlParameter {ParameterName = "AdditiveName", DbType = DbType.String, Value = additive.Name},
                        new MySqlParameter {ParameterName = "Amount", DbType = DbType.Int32, Value = additive.Amount}
                    });

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

        public Task<IEnumerable<Order>> SelectAllPendingOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> SelectAllOrdersForUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderStatusAsync()
        {
            throw new NotImplementedException();
        }
    }
}