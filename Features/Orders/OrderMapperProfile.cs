using AutoMapper;
using ShopAPI.Features.Orders.Model;
using ShopAPI.Features.Orders.RequestHandling.Dto;

namespace ShopAPI.Features.Orders;

public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
    }
}