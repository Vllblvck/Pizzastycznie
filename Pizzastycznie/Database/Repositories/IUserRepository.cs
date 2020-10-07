using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Database.Repositories
{
    public interface IUserRepository
    {
        public bool InsertUser(InsertUserObject userObject);
        public SelectUserObject SelectUser(string email);
        public HashAndSaltObject SelectHashAndSalt(string email);
    }
}