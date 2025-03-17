using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.OrderAgregation
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, List<OrderItem> items, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, decimal subTotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            Items = items;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public List<OrderItem> Items { get; set; } // Navigation property [one]
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; }
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } //Navigation property [one]
        public string  PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
    }

}
