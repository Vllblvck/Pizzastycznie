using System.Collections.Generic;

namespace Pizzastycznie.Database.DTO
{
    public class Food
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public FoodType Type { get; set; }
        public IEnumerable<FoodAdditive> Additives { get; set; }
    }
}