using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Repositories
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePayment(string basketId);
    }
}
