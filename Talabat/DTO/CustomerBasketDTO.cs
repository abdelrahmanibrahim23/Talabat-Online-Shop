using System.Collections.Generic;
using Talabat.Core.Entity;

namespace Talabat.DTO
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public int DeliveryMethodId { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
