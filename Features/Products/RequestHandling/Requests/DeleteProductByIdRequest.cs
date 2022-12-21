using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Requests;

public class DeleteProductByIdRequest : Request<Response>
{
    public DeleteProductByIdRequest(Guid productId)
    {
        ProductId = productId;
    }
    public Guid ProductId { get; set; }
}