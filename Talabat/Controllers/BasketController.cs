using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repositories;
using Talabat.DTO;

namespace Talabat.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,
            IMapper mapper
            )
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> Get(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateAndCreate(CustomerBasketDTO Basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(Basket);
            var basket = await _basketRepository.UpdateBasketAsync(customerBasket);
            return Ok(_mapper.Map<CustomerBasket, CustomerBasketDTO>(basket));
        }
        [HttpDelete]
        public async Task Delete(string id) 
        {
            await _basketRepository.DeletBasketAsync(id);
        }


    }
}
