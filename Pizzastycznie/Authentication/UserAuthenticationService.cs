using System.Threading.Tasks;
using Pizzastycznie.Authentication.DTO;
using Pizzastycznie.Database.DTO;
using Pizzastycznie.Database.Repositories;

namespace Pizzastycznie.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserRegistrationResult> RegisterAsync(UserRegistrationObject registrationData)
        {
            if (await _userRepository.SelectUserAsync(registrationData.Email) != null)
                return UserRegistrationResult.UserExists;

            //TODO add email and password policy            

            var salt = PasswordHashHelper.GenerateSalt();
            var passwordHash = PasswordHashHelper.GenerateHash(registrationData.Password, salt);
            var userObject = new InsertUserObject
            {
                Email = registrationData.Email,
                PasswordHash = passwordHash,
                Salt = salt,
                Name = registrationData.Name,
                Address = registrationData.Address,
                PhoneNumber = registrationData.PhoneNumber,
                IsAdmin = false,
            };

            return await _userRepository.InsertUserAsync(userObject)
                ? UserRegistrationResult.Success
                : UserRegistrationResult.DatabaseError;
        }

        public async Task<UserAuthenticationResult> AuthenticateAsync(UserAuthenticationObject authData)
        {
            var hashAndSalt = await _userRepository.SelectHashAndSaltAsync(authData.Email);

            if (string.IsNullOrWhiteSpace(hashAndSalt.PasswordHash) || string.IsNullOrWhiteSpace(hashAndSalt.Salt))
                return UserAuthenticationResult.InvalidCredentials;

            var hashToCheck = PasswordHashHelper.GenerateHash(authData.Password, hashAndSalt.Salt);
            return hashToCheck.Equals(hashAndSalt.PasswordHash)
                ? UserAuthenticationResult.Success
                : UserAuthenticationResult.InvalidCredentials;
        }
    }
}