using AutoMapper;
using ShopAPI.Features.Products.Model;
using ShopAPI.Features.Products.RequestHandling.Dto;

namespace ShopAPI.Features.Products;

public class ProductsMapperProfile : Profile
{
    public ProductsMapperProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}