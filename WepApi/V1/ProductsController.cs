using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.Common;
using ShopAPI.Features.Products.RequestHandling.Dto;
using ShopAPI.Features.Products.RequestHandling.Requests;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.WepApi.V1;

[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
public class ProductsController : BaseApiController
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Creates new product.
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateProductRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}