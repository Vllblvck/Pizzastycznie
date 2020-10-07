using System;
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

        public UserRegistrationResult Register(UserRegistrationObject registrationData)
        {
            if (_userRepository.SelectUser(registrationData.Email) != null)
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

            return _userRepository.InsertUser(userObject)
                ? UserRegistrationResult.Success
                : UserRegistrationResult.DatabaseError;
        }

        public UserAuthenticationResult Authenticate(UserAuthenticationObject authData)
        {
            var hashAndSalt = _userRepository.SelectHashAndSalt(authData.Email);

            if (string.IsNullOrEmpty(hashAndSalt.PasswordHash) || string.IsNullOrEmpty(hashAndSalt.Salt))
                return UserAuthenticationResult.InvalidCredentials;

            var hashToCheck = PasswordHashHelper.GenerateHash(authData.Password, hashAndSalt.Salt);
            return hashToCheck.Equals(hashAndSalt.PasswordHash)
                ? UserAuthenticationResult.Success
                : UserAuthenticationResult.InvalidCredentials;
        }
    }
}