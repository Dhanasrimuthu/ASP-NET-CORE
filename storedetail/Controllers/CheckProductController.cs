using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using storedetail.model.Dtos;
using storedetail.Repositories;

namespace storedetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CheckProductController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        //[HttpGet]
        //public Task<IActionResult> GetData(string filteron,string filterQuery,int pageNumber,int pageSize) {
        //    var ProductDomainModel = await _productRepository.GetAllCheck(filterOn, filterQuery, pageNumber, pageSize);
        //    return Ok(_mapper.Map<List<ProductDto>>(ProductDomainModel));
        //}
    }
}
