using System.Collections.Generic;
using System;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.DTO
{
    public class OrderDTO
    {
        public string  BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
