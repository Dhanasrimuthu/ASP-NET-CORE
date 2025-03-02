using AutoMapper;
using storedetail.model.domain;
using storedetail.model.Dtos;
using storedetail.Model.Dtos;

namespace storedetail.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<AddProductDto, Product>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<AddCartDto, Cart>().ReverseMap();
            CreateMap<StoreDto, Store>().ReverseMap();
            CreateMap<AddStoreDto, Store>().ReverseMap();
            CreateMap<CartIteamDto, CartItem>().ReverseMap();
            CreateMap<AddCartItemDto, CartItem>().ReverseMap();
        }
    }
}
