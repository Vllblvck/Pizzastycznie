using System.Collections.Generic;
using System.Threading.Tasks;
using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Database.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> InsertOrderAsync(Order order);
        public Task UpdateOrderStatusAsync();
        public Task<IEnumerable<Order>> SelectAllPendingOrdersAsync();
        public Task<IEnumerable<Order>> SelectAllOrdersForUserAsync(string email);
    }
}