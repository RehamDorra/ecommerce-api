using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.RepositoriesContaract;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket == null)
            {
                return BadRequest(new ApiResponse(400) );
            }
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket); 
            var CreatedOrUpdated = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (CreatedOrUpdated == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(CreatedOrUpdated);
        }
        [HttpDelete]
        public async Task DeleteBasket(string Id)
        {
            await _basketRepository.DeleteBasketAsync(Id);
        }
    }
}
