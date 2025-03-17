using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity
{
    public class BasketItem :BaseEntity
    {
        public string ProductName { get; set; }

        public string Description { get; set; }

        public int quantity { get; set; }
        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string Brand { get; set; }
        public string Type { get; set; }

        
    }
}
