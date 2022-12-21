using ShopAPI.Features.Products.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Requests;

public class UpdateProductRequest : Request<Response>
{
    public UpdateProductRequest(UpdateProductDto dto)
    {
        Dto = dto;
    }
    public UpdateProductDto Dto { get; set; }
}