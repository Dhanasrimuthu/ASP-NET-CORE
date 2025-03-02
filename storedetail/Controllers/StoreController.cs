using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using storedetail.model.domain;
using storedetail.model.Dtos;
using storedetail.Model.Domain;
using storedetail.Model.Dtos;
using storedetail.Repositories;
using System.Security.Claims;

namespace storedetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class StoreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StoreController(IMapper mapper,IStoreRepository storeRepository,UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStoreDto addStoreDto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }

            var userId = Guid.Parse(userIdString);

            var store = new Store
            {
                Name = addStoreDto.Name,
                Description = addStoreDto.Description,
                UserId = userId
            };

            await _storeRepository.CreateAsync(store);
            return Ok(_mapper.Map<StoreDto>(store));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] Guid? filterQuery)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }

            var userId = Guid.Parse(userIdString);
            var storeDomainModel = await _storeRepository.GetAllAsync("UserId", userId);
            return Ok(_mapper.Map<List<StoreDto>>(storeDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var storeDomainModel = await _storeRepository.GetByIdAsync(id);
            if (storeDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StoreDto>(storeDomainModel));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, AddStoreDto updateStoreDto)
        {
            

            var  storeDomainModel=_mapper.Map<Store>(updateStoreDto);
            storeDomainModel= await _storeRepository.UpdateAsync(id, storeDomainModel);
            if (storeDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StoreDto>(storeDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var storeDomainModel = await _storeRepository.DeleteAsync(id);
            if (storeDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StoreDto>(storeDomainModel));

        }
    }
}
