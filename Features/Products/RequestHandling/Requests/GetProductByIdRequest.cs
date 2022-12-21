using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Requests;

public class GetProductByIdRequest : Request<Response>
{
    public GetProductByIdRequest(Guid productId)
    {
        ProductId = productId;
    }
    public Guid ProductId { get; set; }
}