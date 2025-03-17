using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryId,OrderAddress ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrderUserAsync(string buyerEmail);
        Task<Order> GGetOrderUserAsync(int orderId, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod();

    }
}
