using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pizzastycznie.Database.DTO.Enums;

namespace Pizzastycznie.Database.DTO
{
    public class Order
    {
        public long Id { get; set; }
        [Required] public long UserId { get; set; }
        public string Comments { get; set; }
        [Required] public string Status { get; set; }
        [Required] public DateTime StatusDate { get; set; }
        [Required] public string CustomerPhone { get; set; }
        [Required] public string DeliveryAddress { get; set; }
        [Required] public PaymentMethod PaymentMethod { get; set; }
        [Required] public decimal TotalPrice { get; set; }
        [Required] public IEnumerable<OrderFood> OrderFood { get; set; }
        public IEnumerable<OrderAdditive> OrderAdditives { get; set; }
    }
}