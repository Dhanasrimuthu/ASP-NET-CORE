using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using storedetail.model.domain;
using storedetail.model.Dtos;
using storedetail.Model.Dtos;
using storedetail.Repositories;
using System.Security.Claims;

namespace storedetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepository cartItemRepository;

        public CartItemController(IMapper mapper,ICartItemRepository cartItemRepository)
        {
            _mapper = mapper;
            this.cartItemRepository = cartItemRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCartItemDto cartIteamDto)
        {
            var CartItemDomainModel= _mapper.Map<CartItem>(cartIteamDto);
            bool itemExists = await cartItemRepository.CartItemExistsAsync(CartItemDomainModel.CartId, CartItemDomainModel.ProductId);

            if (itemExists)
            {
                return Ok("Item already exists in the cart"+itemExists);
            }
            await cartItemRepository.CreateAsync(CartItemDomainModel);
            return Ok(cartIteamDto);    
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status = null)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }
            var userId = Guid.Parse(userIdString);

            var CartItemDomainModel = await cartItemRepository.GetAllAsync("UserId", userId, status);
            return Ok(_mapper.Map<List<CartIteamDto>>(CartItemDomainModel));
        }

        [HttpGet("ByCartId")]
        public async Task<IActionResult> GetById([FromQuery]Guid CartId)
        {
            var CartItemDomainModel=await cartItemRepository.GetByIdAsync(CartId);
            if(CartItemDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map< List<UserProductData>>(CartItemDomainModel));
        }


        /*
                [HttpPut]
                [Route("{id:Guid}")]

                public async Task<IActionResult> Update([FromRoute] Guid id, AddCartItemDto updateCartItemDto)
                {
                    var CartItemDomainModel = _mapper.Map<CartItem>(updateCartItemDto);
                    CartItemDomainModel = await cartItemRepository.UpdateAsync(id, CartItemDomainModel);
                    if (CartItemDomainModel == null)
                    {
                        return NotFound();
                    }
                    return Ok(_mapper.Map<CartIteamDto>(CartItemDomainModel));
                }*/

        [HttpPut]
       // [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromQuery] string status)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }
            var userId = Guid.Parse(userIdString);

            var result = await cartItemRepository.UpdateAsync(id, status, userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var CartItemDomainModel=await cartItemRepository.DeleteAsync(id);
            if(CartItemDomainModel == null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CartIteamDto>(CartItemDomainModel));
        }

    }
}
