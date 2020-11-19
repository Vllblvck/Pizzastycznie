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
                sqlCmd.CommandText = DbProcedures.InsertFood.ProcedureName;
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertFood.Parameters.FoodName,
                        DbType = DbType.String,
                        Value = food.Name
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertFood.Parameters.FoodType,
                        DbType = DbType.Int32,
                        Value = (int) food.Type
                    },
                    new MySqlParameter
                    {
                        ParameterName = DbProcedures.InsertFood.Parameters.FoodPrice,
                        DbType = DbType.Currency,
                        Value = food.Price
                    }
                });

                _logger.LogInformation("Inserting food into database");
                await sqlCmd.ExecuteNonQueryAsync();

                if (food.Additives != null)
                {
                    sqlCmd.CommandText = DbProcedures.InsertFoodAdditive.ProcedureName;
                    foreach (var additive in food.Additives)
                    {
                        _logger.LogInformation("Preparing sql command to insert food additive");

                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddRange(new[]
                        {
                            new MySqlParameter
                            {
                                ParameterName = DbProcedures.InsertFoodAdditive.Parameters.FoodName,
                                DbType = DbType.String,
                                Value = food.Name
                            },
                            new MySqlParameter
                            {
                                ParameterName = DbProcedures.InsertFoodAdditive.Parameters.AdditiveName,
                                DbType = DbType.String,
                                Value = additive.Name
                            },
                            new MySqlParameter
                            {
                                ParameterName = DbProcedures.InsertFoodAdditive.Parameters.AdditivePrice,
                                DbType = DbType.Decimal,
                                Value = additive.Price
                            }
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

        public async Task<Food> SelectFoodAsync(string foodName)
        {
            Food result = null;
            try
            {
                _logger.LogInformation("Opening connection with database");
                await _sqlConn.OpenAsync();

                _logger.LogInformation("Preparing sql command to select food");
                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = DbProcedures.SelectFood.ProcedureName;
                sqlCmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = DbProcedures.SelectFood.Parameters.FoodName,
                    DbType = DbType.String,
                    Value = foodName
                });

                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        result = new Food
                        {
                            Name = sqlReader.GetString(DbTables.Food.FoodName),
                            Type = (FoodType) sqlReader.GetInt32(DbTables.Food.FoodType),
                            Price = sqlReader.GetDecimal(DbTables.Food.Price)
                        };
                    }
                }

                sqlCmd.CommandText = DbProcedures.SelectAdditiveForFood.ProcedureName;
                var additives = new List<FoodAdditive>();

                _logger.LogInformation("Preparing sql command to select additives for food");

                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = DbProcedures.SelectAdditiveForFood.Parameters.FoodName,
                    DbType = DbType.String,
                    Value = foodName
                });

                _logger.LogInformation("Selecting food additives from database");

                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        additives.Add(new FoodAdditive
                        {
                            Name = sqlReader.GetString(DbTables.FoodAdditives.AdditiveName),
                            Price = sqlReader.GetDecimal(DbTables.FoodAdditives.Price)
                        });
                    }
                }

                if (result != null) result.Additives = additives;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while selecting food from database: {ex.Message}");
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
                sqlCmd.CommandText = DbProcedures.SelectAllFood.ProcedureName;

                _logger.LogInformation("Selecting food from database");

                await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await sqlReader.ReadAsync())
                    {
                        result.Add(new Food
                        {
                            Name = sqlReader.GetString(DbTables.Food.FoodName),
                            Type = (FoodType) sqlReader.GetInt32(DbTables.Food.FoodType),
                            Price = sqlReader.GetDecimal(DbTables.Food.Price),
                        });
                    }
                }

                sqlCmd.CommandText = DbProcedures.SelectAdditiveForFood.ProcedureName;
                foreach (var food in result)
                {
                    var additives = new List<FoodAdditive>();

                    _logger.LogInformation("Preparing sql command to select additives for food");

                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = DbProcedures.SelectAdditiveForFood.Parameters.FoodName,
                        DbType = DbType.String,
                        Value = food.Name
                    });

                    _logger.LogInformation("Selecting food additives from database");

                    await using (var sqlReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlReader.ReadAsync())
                        {
                            additives.Add(new FoodAdditive
                            {
                                Name = sqlReader.GetString(DbTables.FoodAdditives.AdditiveName),
                                Price = sqlReader.GetDecimal(DbTables.FoodAdditives.Price)
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
                sqlCmd.CommandText = DbProcedures.DeleteFood.ProcedureName;
                sqlCmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = DbProcedures.DeleteFood.Parameters.FoodName,
                    DbType = DbType.String,
                    Value = foodName
                });

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