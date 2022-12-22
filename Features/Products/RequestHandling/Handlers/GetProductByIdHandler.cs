using MediatR;
using ShopAPI.Features.Products.RequestHandling.Requests;
using ShopAPI.Features.Products.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, Response>
{
    private readonly IPaymentService _productService;
    public GetProductByIdHandler(IPaymentService productService)
    {
        _productService = productService;
    }

    public async Task<Response> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productService.GetProductById(request.ProductId, cancellationToken);
            return Response.Ok(request.Id, result);
        }
        catch (Exception e)
        {
            return Response.InternalServerError(request.Id, e);
        }
    }
}