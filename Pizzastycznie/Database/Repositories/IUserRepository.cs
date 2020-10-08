using System.Threading.Tasks;
using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Database.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> InsertUserAsync(InsertUserObject userObject);
        public Task<SelectUserObject> SelectUserAsync(string email);
        public Task<HashAndSaltObject> SelectHashAndSaltAsync(string email);
    }
}