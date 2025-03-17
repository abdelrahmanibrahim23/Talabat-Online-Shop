using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.OrderAgregation
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrder productItemOrder, decimal price, int quantity)
        {
            Product = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
