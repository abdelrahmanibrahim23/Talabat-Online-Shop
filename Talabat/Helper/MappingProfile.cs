using AutoMapper;
using Talabat.Core.Entity;
using Talabat.Core.Entity.OrderAgregation;
using Talabat.DTO;

namespace Talabat.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductReturnDTO>().ForMember(d=>d.ProductBreand,o=>o.MapFrom(s=>s.ProductBreand.Name))
                .ForMember(d => d.ProductTypes, o => o.MapFrom(s => s.ProductTypes.Name))
                .ForMember(p=>p.PictureUrl,o=>o.MapFrom<ProductPictureUrlReslove>());
            CreateMap<AddressDto, Core.Entity.Identity.Address>().ReverseMap();
            CreateMap<CustomerBasketDTO,CustomerBasket>().ReverseMap();
            CreateMap<AddressDto, Core.Entity.OrderAgregation.OrderAddress>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(O => O.DeliveryMethod, O => O.MapFrom(O => O.DeliveryMethod.ShortName))
                .ForMember(O => O.DeliveryMethodCost, O => O.MapFrom(O => O.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(O => O.ProductId, O => O.MapFrom(O => O.Product.ProductId))
                .ForMember(O => O.ProductName, O => O.MapFrom(O => O.Product.ProductName))
                .ForMember(O => O.ProductId, O => O.MapFrom(O => O.Product.ProductId))
                .ForMember(O => O.ProductUrl, O => O.MapFrom(O => O.Product.ProductUrl))
                .ForMember(O => O.ProductUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());






        }
    }
}
