namespace Pizzastycznie.Database.DTO
{
    public class SelectUserObject
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}