using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.Core.Specification
{
    public class OrderWithItemAndDeliverySpecification:BaseSpecification<Order>
    {
        public OrderWithItemAndDeliverySpecification(int id,string buyerEmail) :
            base(O => O.BuyerEmail == buyerEmail && O.Id==id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            
        }
        public OrderWithItemAndDeliverySpecification(string buyerEmail):base(O=>O.BuyerEmail==buyerEmail) {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O=>O.Items);
            AddOrderByDescending(O=>O.OrderDate);
        }

    }
}
