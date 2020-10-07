namespace Pizzastycznie.Database.DTO
{
    public class HashAndSaltObject
    {
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}