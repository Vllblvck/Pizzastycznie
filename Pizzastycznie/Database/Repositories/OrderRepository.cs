using System;
using System.Collections.Generic;
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

        public Task<bool> InsertOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> SelectAllOrdersAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}