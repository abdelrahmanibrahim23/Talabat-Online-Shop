using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repositories;
using Talabat.DTO;
using Talabat.Errors;

namespace Talabat.Controllers
{
    
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService,
            IMapper mapper
            ) 
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePayment(string basketId)
        {
            var payment = await _paymentService.CreateOrUpdatePayment(basketId);
            if (payment == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<CustomerBasket, CustomerBasketDTO>(payment));
        }
    }
}
