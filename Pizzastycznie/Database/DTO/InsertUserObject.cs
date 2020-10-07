namespace Pizzastycznie.Database.DTO
{
    public class InsertUserObject
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}