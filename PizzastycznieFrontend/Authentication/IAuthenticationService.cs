using System.Threading.Tasks;
using PizzastycznieFrontend.Authentication.DTO;

namespace PizzastycznieFrontend.Authentication
{
    public interface IAuthenticationService
    {
        public Task<bool> AuthenticateAsync(UserCredentialsObject userCredentials);
    }
}