using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entity;
using Talabat.DTO;

namespace Talabat.Helper
{
    public class ProductPictureUrlReslove : IValueResolver<Product, ProductReturnDTO, string>
    {

        public IConfiguration Configuration { get; }
        public ProductPictureUrlReslove(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public string Resolve(Product source, ProductReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
