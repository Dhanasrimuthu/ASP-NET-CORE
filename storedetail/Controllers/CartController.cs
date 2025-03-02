using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
   // [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public CartController(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] Guid? filterQuery, [FromQuery] string? status)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }
            var userId = Guid.Parse(userIdString);
            var cartDomainModel = await _cartRepository.GetAll("UserId", userId, status);
            return Ok(_mapper.Map<List<CartDto>>(cartDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }
            var userId = Guid.Parse(userIdString);

            var cart = new Cart
            {
                UserId = userId,
                Status="Active",
                CreatedDate = DateTime.Now
            };

            await _cartRepository.CreateAsync(cart);
            return Ok(new { Message = "Cart created successfully", id = cart.Id });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }
            var userId = Guid.Parse(userIdString);

            var cartDomainModel = await _cartRepository.GetByIdAsync(id);
            if (cartDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CartDto>(cartDomainModel));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddCartDto updateCartDto)
        {
            var cartDomainModel = _mapper.Map<Cart>(updateCartDto);
            cartDomainModel = await _cartRepository.UpdateAsync(cartDomainModel, id);
            if (cartDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CartDto>(cartDomainModel));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var cartDomainModel = await _cartRepository.DeleteAsync(id);
            if (cartDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CartDto>(cartDomainModel));
        }
    }
}
