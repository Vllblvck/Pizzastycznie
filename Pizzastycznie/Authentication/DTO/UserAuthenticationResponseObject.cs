namespace Pizzastycznie.Authentication.DTO
{
    public class UserAuthenticationResponseObject
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string ExpirationDate { get; set; }
    }
}