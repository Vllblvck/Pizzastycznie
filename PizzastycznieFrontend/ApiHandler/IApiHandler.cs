using System.Collections.Generic;
using System.Threading.Tasks;
using PizzastycznieFrontend.ApiHandler.DTO;
using PizzastycznieFrontend.ApiHandler.DTO.Enums;

namespace PizzastycznieFrontend.ApiHandler
{
    public interface IApiHandler
    {
        public Task<IEnumerable<Food>> GetMenuItemsAsync();
        public Task<AddMenuItemResult> AddMenuItemAsync(Food food);
        public Task DeleteMenuItemAsync(string foodName);
    }
}