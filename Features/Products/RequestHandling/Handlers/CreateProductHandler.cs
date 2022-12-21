using MediatR;
using ShopAPI.Features.Products.RequestHandling.Requests;
using ShopAPI.Features.Products.Services;
using ShopAPI.Features.RequestHandling.Base;
using Exception = System.Exception;

namespace ShopAPI.Features.Products.RequestHandling.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, Response>
{
    private readonly IProductService _productService;
    public CreateProductHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Response> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _productService.CreateProduct(request.Dto, cancellationToken);
            return Response.Ok(request.Id);
        }
        catch (Exception e)
        {
            return Response.InternalServerError(request.Id, e);
        }
    }
}