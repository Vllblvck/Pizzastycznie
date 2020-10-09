using System.Collections.Generic;
using System.Threading.Tasks;
using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Database.Repositories
{
    public interface IFoodRepository
    {
        public Task<bool> InsertFoodAsync(Food food);
        public Task<IEnumerable<Food>> SelectAllFoodAsync();
        public Task<bool> DeleteFoodAsync(string foodName);
    }
}