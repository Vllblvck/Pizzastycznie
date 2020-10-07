using Pizzastycznie.Authentication.DTO;

namespace Pizzastycznie.Authentication
{
    public interface IUserAuthenticationService
    {
        public UserRegistrationResult Register(UserRegistrationObject userAuthentication);
        public UserAuthenticationResult Authenticate(UserAuthenticationObject authData);
    }
}