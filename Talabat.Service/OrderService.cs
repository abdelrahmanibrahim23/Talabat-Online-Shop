using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.OrderAgregation;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specification;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethod;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService
            //IGenericRepository<Product> productRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethod,
            //IGenericRepository<Order> orderRepo
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepo = productRepo;
            //_deliveryMethod = deliveryMethod;
            //_orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryId, OrderAddress ShippingAddress)
        {
            //Get Basket from Basket Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //Get selected Item at Basket from product Repo
            var orderItems =new List<OrderItem>();
            foreach (var item in basket.Items) { 
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.quantity);
                orderItems.Add(orderItem);
            }
            //calculate SubTotal
            var subTotal = orderItems.Sum(item=>item.Price * item.Quantity);
            //Get Delivery Method from DeliveryMethod Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryId);
            //Create order 
            var spec = new OrderPaymentIntentSpecification(basket.PaymentIntentId);
            var existOrder = await _unitOfWork.Repository<Order>().GetAllSpecByIdAsync(spec);
                if(existOrder != null)
                  {
                    _unitOfWork.Repository<Order>().DeleteAsync(existOrder);
                    await _paymentService.CreateOrUpdatePayment(basketId);

                   }
            var order = new Order(buyerEmail, orderItems, ShippingAddress, deliveryMethod, subTotal,basket.PaymentIntentId);
             await _unitOfWork.Repository<Order>().CreateAsync(order);
            //save to database[TODO]
            var result = await _unitOfWork.Complete();
            if(result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod()
        {
            var deliverymethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliverymethods;
        }

        public async Task<IReadOnlyList<Order>> GetOrderUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliverySpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllSpecAsync(spec);
            return orders;
        }

        public async Task<Order> GGetOrderUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemAndDeliverySpecification(orderId, buyerEmail);
            var order =await _unitOfWork.Repository<Order>().GetAllSpecByIdAsync(spec);
            return order;

        }
    }
}
