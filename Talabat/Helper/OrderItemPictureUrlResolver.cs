using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entity.OrderAgregation;
using Talabat.DTO;

namespace Talabat.Helper
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public IConfiguration Configuration { get; }
        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductUrl))
                return $"{Configuration["BaseApiUrl"]}{source.Product.ProductUrl}";
            return null;
        }
    }
}
