using System;

namespace Pizzastycznie.Database.DTO
{
    public class Order
    {
        public UserOrderData UserOrderData { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public bool SelfPickup { get; set; }
    }
}