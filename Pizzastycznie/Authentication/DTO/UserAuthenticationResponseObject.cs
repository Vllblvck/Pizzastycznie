namespace Pizzastycznie.Authentication.DTO
{
    public class UserAuthenticationResponseObject
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string ExpirationDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}