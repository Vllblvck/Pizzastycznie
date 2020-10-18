using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Pizzastycznie.Database.DTO;
using Pizzastycznie.Database.Repositories.Interfaces;

namespace Pizzastycznie.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<IUserRepository> _logger;
        private readonly MySqlConnection _sqlConn;

        public UserRepository(ILogger<IUserRepository> logger)
        {
            _logger = logger;
            _sqlConn = new MySqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

        public async Task<bool> InsertUserAsync(InsertUserObject userObject)
        {
            bool result;
            try
            {
                _logger.LogInformation("Preparing sql command to insert user");

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "InsertUser";
                sqlCmd.Parameters.AddRange(new[]
                {
                    new MySqlParameter {ParameterName = "Email", DbType = DbType.String, Value = userObject.Email},
                    new MySqlParameter {ParameterName = "Name", DbType = DbType.String, Value = userObject.Name},
                    new MySqlParameter
                        {ParameterName = "PasswordHash", DbType = DbType.String, Value = userObject.PasswordHash},
                    new MySqlParameter {ParameterName = "Salt", DbType = DbType.String, Value = userObject.Salt},
                    new MySqlParameter {ParameterName = "Address", DbType = DbType.String, Value = userObject.Address},
                    new MySqlParameter
                        {ParameterName = "PhoneNumber", DbType = DbType.String, Value = userObject.PhoneNumber},
                    new MySqlParameter {ParameterName = "Admin", DbType = DbType.Boolean, Value = userObject.IsAdmin},
                });

                _logger.LogInformation("Inserting user into database");

                await _sqlConn.OpenAsync();
                await sqlCmd.ExecuteNonQueryAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while inserting user: {ex.Message}");
                result = false;
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    await _sqlConn.CloseAsync();
            }

            return result;
        }

        public async Task<SelectUserObject> SelectUserAsync(string email)
        {
            SelectUserObject result = null;
            try
            {
                await _sqlConn.OpenAsync();

                _logger.LogInformation("Preparing sql command to select user");

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "SelectUser";
                sqlCmd.Parameters.Add(new MySqlParameter
                    {ParameterName = "UserEmail", DbType = DbType.String, Value = email});

                _logger.LogInformation("Selecting user from database");

                await using var sqlReader = await sqlCmd.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    var addressOrdinal = sqlReader.GetOrdinal("address");
                    var phoneNumberOrdinal = sqlReader.GetOrdinal("phone_number");

                    result = new SelectUserObject
                    {
                        Id = sqlReader.GetInt64("id"),
                        Email = sqlReader.GetString("email"),
                        Name = sqlReader.GetString("name"),
                        IsAdmin = sqlReader.GetBoolean("admin"),
                        
                        Address = await sqlReader.IsDBNullAsync(addressOrdinal)
                            ? null : sqlReader.GetString("address"),
                        PhoneNumber = await sqlReader.IsDBNullAsync(phoneNumberOrdinal)
                            ? null : sqlReader.GetString("phone_number")
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while selecting user from database: {ex.Message}");
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    await _sqlConn.CloseAsync();
            }

            return result;
        }

        public async Task<HashAndSaltObject> SelectHashAndSaltAsync(string email)
        {
            var result = new HashAndSaltObject();
            try
            {
                _logger.LogInformation("Preparing sql command to select hash and salt");

                var sqlCmd = _sqlConn.CreateCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "SelectHashAndSalt";
                sqlCmd.Parameters.Add(new MySqlParameter
                    {ParameterName = "Email", DbType = DbType.String, Value = email});

                _logger.LogInformation("Selecting hash and salt from database");
                await _sqlConn.OpenAsync();

                await using var sqlReader = await sqlCmd.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    result.PasswordHash = sqlReader.GetString("password_hash");
                    result.Salt = sqlReader.GetString("salt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while selecting hash and salt from database: {ex.Message}");
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