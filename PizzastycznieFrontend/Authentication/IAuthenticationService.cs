using System.Threading.Tasks;

namespace PizzastycznieFrontend.Authentication
{
    public interface IAuthenticationService
    {
        public Task<bool> Authenticate(UserCredentialsObject userCredentials);
    }
}