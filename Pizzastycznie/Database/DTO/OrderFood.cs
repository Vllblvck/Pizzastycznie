using System.ComponentModel.DataAnnotations;

namespace Pizzastycznie.Database.DTO
{
    public class OrderFood
    {
        [Required] public string Name { get; set; }
        [Required] public int Amount { get; set; }
    }
}