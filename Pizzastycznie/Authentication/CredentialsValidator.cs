using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Pizzastycznie.Authentication
{
    public static class CredentialsValidator
    {
        private static readonly Regex NameRegex = new Regex(@"^[a-z A-Z]+$");

        private static readonly Regex PasswordRegex =
            new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            return PasswordRegex.IsMatch(password);
        }

        public static bool IsValidName(string username)
        {
            return NameRegex.IsMatch(username);
        }
    }
}