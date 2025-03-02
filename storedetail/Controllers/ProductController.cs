using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using storedetail.model.domain;
using storedetail.model.Dtos;
using storedetail.Model.Dtos;
using storedetail.Repositories;
using System.Collections.Generic;
using System.Security.Claims;

namespace storedetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductController(IMapper mapper,IProductRepository ProductRepository)
        {
            _mapper = mapper;
            _productRepository = ProductRepository;
        }

        [HttpPost]

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddProductDto addProductDto)
        {
            var ProductDomainModel =_mapper.Map<Product>(addProductDto);

            await _productRepository.CreateAsync(ProductDomainModel);

            return Ok(addProductDto);
        }
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetUserData([FromQuery] string? status)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("Unauthorized id is null");
            }
            var userId = Guid.Parse(userIdString);

            var ProductDomainModel= await _productRepository.GetUserDataAsync(userId,status);
            return Ok(_mapper.Map<List<UserProductData[]>>(ProductDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 40)
        {
            var ProductDomainModel=await _productRepository.GetAllAsync(filterOn,filterQuery, pageNumber ,  pageSize);
            return Ok(_mapper.Map<List<ProductDto>>(ProductDomainModel));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var ProductDomainModel = await _productRepository.GetByIdAsync(id);
            if (ProductDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(ProductDomainModel));

        }
        [HttpPut]
        [Route("{id:Guid}")]

       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute]Guid id, AddProductDto updateProductDto)
        {
            var ProductDomainModel=_mapper.Map<Product>(updateProductDto);
            ProductDomainModel=await _productRepository.UpdateAsync(id, ProductDomainModel);
            if (ProductDomainModel == null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(ProductDomainModel));
        }



        [HttpDelete]
        [Route("{id:Guid}")]

       // [Authorize(Roles = "Admin")]
        public async  Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var ProductDomainModel = await _productRepository.DeleteAsync(id);
            if (ProductDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(ProductDomainModel));



        }
    }
}
