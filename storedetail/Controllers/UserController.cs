using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using storedetail.model.Dtos;
using storedetail.Repositories;

namespace storedetail.Controllers 
{
    public class UserController : Controller
    {
        /*[HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] Guid? filterQuery)
        {
            *//*var storeDomainModel = await _storeRepository.GetAllAsync(filterOn, filterQuery);
            return Ok(_mapper.Map<List<StoreDto>>(storeDomainModel));*//*
        }*/

    }
}
