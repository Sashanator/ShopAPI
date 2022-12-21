using ShopAPI.Features.Common;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Requests;

public class GetProductsRequest : Request<Response>
{
    public GetProductsRequest(PaginationDto dto)
    {
        Dto = dto;
    }
    public PaginationDto Dto { get; set; }
}