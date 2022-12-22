using MediatR;
using ShopAPI.Features.Products.RequestHandling.Requests;
using ShopAPI.Features.Products.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Handlers;

public class GetProductsHandler : IRequestHandler<GetProductsRequest, Response>
{
    private readonly IPaymentService _productService;
    public GetProductsHandler(IPaymentService productService)
    {
        _productService = productService;
    }

    public async Task<Response> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productService.GetProducts(request.Dto, cancellationToken);
            return Response.Ok(request.Id, result);
        }
        catch (Exception e)
        {
            return Response.InternalServerError(request.Id, e);
        }
    }
}