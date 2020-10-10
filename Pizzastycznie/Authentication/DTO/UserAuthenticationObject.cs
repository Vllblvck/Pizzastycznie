using System.ComponentModel.DataAnnotations;

namespace Pizzastycznie.Authentication.DTO
{
    public class UserAuthenticationObject
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}