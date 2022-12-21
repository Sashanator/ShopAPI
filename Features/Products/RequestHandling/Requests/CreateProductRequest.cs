using ShopAPI.Features.Products.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Requests;

public class CreateProductRequest : Request<Response>
{
    public CreateProductRequest(CreateProductDto dto)
    {
        Dto = dto;
    }
    public CreateProductDto Dto { get; set; }
}