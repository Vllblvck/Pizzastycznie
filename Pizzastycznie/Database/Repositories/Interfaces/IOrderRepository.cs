using System.Collections.Generic;
using System.Threading.Tasks;
using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Database.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> InsertOrderAsync(Order order);
        public Task<IEnumerable<Order>> SelectAllOrdersAsync(string email);
    }
}