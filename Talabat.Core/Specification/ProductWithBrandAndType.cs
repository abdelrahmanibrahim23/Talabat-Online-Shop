using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandAndType : BaseSpecification<Product>
    {
        public ProductWithBrandAndType(ProductSpecPrams productPrams)
            :base(
                 p=>(string.IsNullOrEmpty(productPrams.Search)|| p.Name.Contains(productPrams.Search.ToLower())&&
                 !productPrams.BrandId.HasValue||p.ProductBreandId== productPrams.BrandId) &&
                 (!productPrams.TypeId.HasValue||p.ProductTypeId== productPrams.TypeId)
                 )

        {
            Includes.Add(P => P.ProductTypes);
            Includes.Add(p=>p.ProductBreand);
            AddPagination(productPrams.PageSize *(productPrams.PageIndex-1), productPrams.PageSize);
            if (!string.IsNullOrEmpty(productPrams.Sort))
            {
                switch (productPrams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                            break;
                    case "PriceDes":
                        AddOrderByDescending(p=>p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
        public ProductWithBrandAndType(int id):base(p=>p.Id==id)
        {
            Includes.Add(P => P.ProductTypes);
            Includes.Add(p => p.ProductBreand);

        }
    }
}
