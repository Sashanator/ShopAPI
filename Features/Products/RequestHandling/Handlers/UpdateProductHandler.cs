using MediatR;
using ShopAPI.Features.Products.RequestHandling.Requests;
using ShopAPI.Features.Products.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Products.RequestHandling.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Response>
{
    private readonly IProductService _productService;
    public UpdateProductHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Response> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _productService.UpdateProduct(request.Dto, cancellationToken);
            return Response.Ok(request.Id);
        }
        catch (Exception e)
        {
            return Response.InternalServerError(request.Id, e);
        }
    }
}