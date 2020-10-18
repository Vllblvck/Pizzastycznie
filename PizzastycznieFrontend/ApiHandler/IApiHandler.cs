using System.Collections.Generic;
using System.Threading.Tasks;
using PizzastycznieFrontend.ApiHandler.DTO;

namespace PizzastycznieFrontend.ApiHandler
{
    public interface IApiHandler
    {
        public Task<IEnumerable<Food>> GetMenuItemsAsync();
        public Task<bool> PlaceOrderAsync(Order order);
        public Task<IEnumerable<Order>> GetOrderHistoryAsync(string email);
    }
}