using System;
using System.Threading.Tasks;
using Pizzastycznie.Authentication.DTO;
using Pizzastycznie.Database.DTO;
using Pizzastycznie.Database.Repositories.Interfaces;

namespace Pizzastycznie.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly DateTimeOffset _tokenExpirationDate;
        private readonly IUserRepository _userRepository;

        public UserAuthenticationService(IUserRepository userRepository)
        {
            _tokenExpirationDate = new DateTimeOffset(DateTime.Now).AddHours(1);
            _userRepository = userRepository;
        }

        public async Task<UserRegistrationResult> RegisterAsync(UserRegistrationObject registrationData)
        {
            if (await _userRepository.SelectUserAsync(registrationData.Email) != null)
                return UserRegistrationResult.UserExists;

            if (!CredentialsValidator.IsValidEmail(registrationData.Email))
                return UserRegistrationResult.BadEmail;

            if (!CredentialsValidator.IsValidPassword(registrationData.Password))
                return UserRegistrationResult.BadPassword;

            if (!CredentialsValidator.IsValidName(registrationData.Name))
                return UserRegistrationResult.BadName;

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

        public async Task<UserAuthenticationResponseObject> AuthenticateAsync(UserAuthenticationObject authData)
        {
            var hashAndSalt = await _userRepository.SelectHashAndSaltAsync(authData.Email);

            if (string.IsNullOrWhiteSpace(hashAndSalt.PasswordHash) || string.IsNullOrWhiteSpace(hashAndSalt.Salt))
                return null;

            var hashToCheck = PasswordHashHelper.GenerateHash(authData.Password, hashAndSalt.Salt);
            if (!hashToCheck.Equals(hashAndSalt.PasswordHash)) return null;
            
            var user = await _userRepository.SelectUserAsync(authData.Email);
            return new UserAuthenticationResponseObject
            {
                Token = TokenGenerator.GenerateToken(user, _tokenExpirationDate),
                ExpirationDate = _tokenExpirationDate.ToString(),
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin
            };

        }
    }
}
