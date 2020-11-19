using System;
using System.Collections.Generic;
using PizzastycznieFrontend.ApiHandler.DTO.Enums;

namespace PizzastycznieFrontend.ApiHandler.DTO
{
    public class Order
    {
        public long UserId { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderFood> OrderFood { get; set; }
        public IEnumerable<OrderAdditive> OrderAdditives { get; set; }
    }
}