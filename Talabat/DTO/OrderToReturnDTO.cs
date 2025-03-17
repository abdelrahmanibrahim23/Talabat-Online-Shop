using System.Collections.Generic;
using System;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.DTO
{
    public class OrderToReturnDTO
    {
        public string BuyerEmail { get; set; }
        public List<OrderItemDto> Items { get; set; } 
        public DateTimeOffset OrderDate { get; set; } 
        public string OrderStatus { get; set; }
        public OrderAddress ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public string paymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
