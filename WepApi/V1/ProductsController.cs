using System.Net;
using EmployeeService.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.Common;
using ShopAPI.Features.Products.Model;
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
    public async Task<IActionResult> CreateProduct([FromBody] CreatePaymentDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateProductRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Updates product
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdatePaymentDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateProductRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Deletes product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteProductByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns paged result of products
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Product>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts([FromQuery] PaginationDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductsRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}