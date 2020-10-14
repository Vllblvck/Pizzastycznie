using System.Collections.Generic;
using System.Threading.Tasks;
using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Database.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> InsertOrderAsync(Order order);
        public Task<bool> UpdateOrderStatusAsync(long orderId, string status);
        public Task<IEnumerable<Order>> SelectPendingOrdersAsync();
        public Task<IEnumerable<Order>> SelectOrdersForUserAsync(string email);
    }
}