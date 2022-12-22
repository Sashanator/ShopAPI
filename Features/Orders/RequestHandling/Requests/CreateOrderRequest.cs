using ShopAPI.Features.Orders.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Orders.RequestHandling.Requests;

public class CreateOrderRequest : Request<Response>
{
    public CreateOrderRequest(CreateOrderDto dto)
    {
        Dto = dto;
    }
    public CreateOrderDto Dto { get; set; }
}