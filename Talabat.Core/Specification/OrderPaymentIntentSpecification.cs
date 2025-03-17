using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.OrderAgregation;

namespace Talabat.Core.Specification
{
    public class OrderPaymentIntentSpecification :BaseSpecification<Order>
    {
        public OrderPaymentIntentSpecification(string paymentIntentId):base(O=>O.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
