using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.OrderAgregation;
using Talabat.Core.Repositories;
using Product = Talabat.Core.Entity.Product;

namespace Talabat.Service
{
    [Authorize]
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork
            )
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePayment(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSecretKey:Secretkey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
                basket.ShippingPrice = shippingPrice;

            }
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price!= product.Price)
                    item.Price = product.Price; 

            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (String.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(iteam => iteam.Price * iteam.quantity * 100) +(long) shippingPrice * 100,
                    Currency ="usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };
                intent = await service.CreateAsync(option);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret= intent.ClientSecret;

            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                     Amount = (long)basket.Items.Sum(iteam => iteam.Price * iteam.quantity * 100) +(long) shippingPrice * 100,

                };
                intent = await service.UpdateAsync(basket.PaymentIntentId,option);
            }
             await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
