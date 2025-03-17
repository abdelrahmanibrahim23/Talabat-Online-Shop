using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entity.OrderAgregation;
using Talabat.Core.Services;
using Talabat.DTO;
using Talabat.Errors;

namespace Talabat.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,
            IMapper mapper
            )
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var shippingAddress = _mapper.Map<AddressDto, OrderAddress>(orderDTO.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDTO.BasketId, orderDTO.DeliveryMethodId, shippingAddress);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnDTO>(order));
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderUserAsync(buyerEmail);
            return Ok(_mapper.Map< IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrdersForUserById(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GGetOrderUserAsync(id, buyerEmail);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDTO>(order));
        }
        [HttpGet("deliverymethods")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethods()
        {
            var deliverymethods = await _orderService.GetDeliveryMethod();
            return Ok(deliverymethods);
        }

    }
}

