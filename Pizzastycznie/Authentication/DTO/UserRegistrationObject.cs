using System.ComponentModel.DataAnnotations;

namespace Pizzastycznie.Authentication.DTO
{
    public class UserRegistrationObject
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}