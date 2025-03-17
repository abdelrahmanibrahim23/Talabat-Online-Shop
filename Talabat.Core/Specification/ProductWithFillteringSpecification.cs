using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specification
{
    public class ProductWithFillteringSpecification :BaseSpecification<Product>
    {
        public ProductWithFillteringSpecification(ProductSpecPrams productPrams) :
            base(p => (string.IsNullOrEmpty(productPrams.Search) || p.Name.Contains(productPrams.Search.ToLower()) &&
            !productPrams.BrandId.HasValue || p.ProductBreandId == productPrams.BrandId) &&
                 (!productPrams.TypeId.HasValue || p.ProductTypeId == productPrams.TypeId))
        { 

        }
    }
}
