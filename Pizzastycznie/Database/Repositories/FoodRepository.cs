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
    public class FoodRepository : IFoodRepository
    {
        private readonly ILogger<IFoodRepository> _logger;
        private readonly MySqlConnection _sqlConn;

        public FoodRepository(ILogger<IFoodRepository> logger)
        {
            _logger = logger;
            _sqlConn = new MySqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

        //TODO Take care of duplicate entry error
        public async Task<bool> InsertFoodAsync(Food food)
        {
            _logger.LogInformation("Preparing sql command to insert food");

            MySqlTransaction transaction = null;
            bool result;
            try
            {
                await _sqlConn.OpenAsync();
                transaction = await _sqlConn.BeginTransactionAsync();

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "InsertFood";
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter {ParameterName = "Name", DbType = DbType.String, Value = food.Name},
                    new MySqlParameter {ParameterName = "Type", DbType = DbType.Int32, Value = (int) food.Type},
                    new MySqlParameter {ParameterName = "Price", DbType = DbType.Currency, Value = food.Price},
                });

                _logger.LogInformation("Inserting food into database");
                await sqlCmd.ExecuteNonQueryAsync();

                if (food.Additives != null)
                {
                    sqlCmd.CommandText = "InsertFoodAdditive";
                    foreach (var additive in food.Additives)
                    {
                        _logger.LogInformation("Preparing sql command to insert food additive");

                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddRange(new[]
                        {
                            new MySqlParameter {ParameterName = "FoodName", DbType = DbType.String, Value = food.Name},
                            new MySqlParameter
                                {ParameterName = "AdditiveName", DbType = DbType.String, Value = additive.Name},
                            new MySqlParameter
                                {ParameterName = "Price", DbType = DbType.Decimal, Value = additive.Price},
                        });

                        _logger.LogInformation("Inserting food additive into database");
                        await sqlCmd.ExecuteNonQueryAsync();
                    }
                }

                await transaction.CommitAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while inserting food into database: {ex.Message}");

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

        public async Task<IEnumerable<Food>> SelectAllFoodAsync()
        {
            MySqlTransaction transaction = null;
            var result = new List<Food>();
            try
            {
                await _sqlConn.OpenAsync();
                transaction = await _sqlConn.BeginTransactionAsync();

                _logger.LogInformation("Preparing sql command to select food");

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "SelectAllFood";

                _logger.LogInformation("Selecting food from database");

                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        result.Add(new Food
                        {
                            Name = sqlReader.GetString("food_name"),
                            Type = (FoodType) sqlReader.GetInt32("food_type"),
                            Price = sqlReader.GetDecimal("price"),
                        });
                    }
                }

                sqlCmd.CommandText = "SelectAdditiveForFood";
                foreach (var food in result)
                {
                    var additives = new List<FoodAdditive>();

                    _logger.LogInformation("Preparing sql command to select additives for food");

                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add(new MySqlParameter
                        {ParameterName = "FoodName", DbType = DbType.String, Value = food.Name});

                    _logger.LogInformation("Selecting food additives from database");

                    await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlReader.ReadAsync())
                        {
                            additives.Add(new FoodAdditive
                            {
                                Name = sqlReader.GetString("additive_name"),
                                Price = sqlReader.GetDecimal("price")
                            });
                        }
                    }

                    food.Additives = additives;
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while selecting food from database: {ex.Message}");

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

        public async Task<bool> DeleteFoodAsync(string foodName)
        {
            bool result;
            try
            {
                _logger.LogInformation("Preparing sql command to delete food");

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "DeleteFood";
                sqlCmd.Parameters.Add(new MySqlParameter
                    {ParameterName = "FoodName", DbType = DbType.String, Value = foodName});

                _logger.LogInformation("Deleting food from database");

                await _sqlConn.OpenAsync();
                await sqlCmd.ExecuteNonQueryAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while deleting food from database: {ex.Message}");
                result = false;
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    await _sqlConn.CloseAsync();
            }

            return result;
        }
    }
}