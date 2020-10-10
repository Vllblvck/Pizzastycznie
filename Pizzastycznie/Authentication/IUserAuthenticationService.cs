using System.Threading.Tasks;
using Pizzastycznie.Authentication.DTO;

namespace Pizzastycznie.Authentication
{
    public interface IUserAuthenticationService
    {
        public Task<UserRegistrationResult> RegisterAsync(UserRegistrationObject userAuthentication);
        public Task<UserAuthenticationResponseObject> AuthenticateAsync(UserAuthenticationObject authData);
    }
}