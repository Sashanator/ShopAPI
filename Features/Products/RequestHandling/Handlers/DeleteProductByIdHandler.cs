using MediatR;
using ShopAPI.Features.Products.RequestHandling.Requests;
using ShopAPI.Features.Products.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Handlers;

/// <inheritdoc />
public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest, Response>
{
    private readonly IPaymentService _productService;
    public DeleteProductByIdHandler(IPaymentService productService)
    {
        _productService = productService;
    }

    public async Task<Response> Handle(DeleteProductByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _productService.DeleteProduct(request.ProductId, cancellationToken);
            return Response.Ok(request.Id);
        }
        catch (Exception e)
        {
            return Response.InternalServerError(request.Id, e);
        }
    }
}