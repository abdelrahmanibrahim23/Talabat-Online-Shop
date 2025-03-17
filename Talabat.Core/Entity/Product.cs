using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public int ProductBreandId { get; set; }

        public ProductBreand ProductBreand { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductTypes { get; set; }
    }
}
