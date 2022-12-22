using System.Net;
using EmployeeService.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.Common;
using ShopAPI.Features.Orders.Model;
using ShopAPI.Features.Orders.RequestHandling.Dto;
using ShopAPI.Features.Orders.RequestHandling.Requests;
using ShopAPI.Features.Products.Model;
using ShopAPI.Features.Products.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.WepApi.V1;

[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
public class OrdersController : BaseApiController
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Creates order
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateOrderRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Adds products to order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="productIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/add-products")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddProductsToOrder(Guid id, [FromBody] List<ProductsDto> products,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AddProductsToOrderRequest(products, id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Deletes order by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteOrderById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteOrderByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns order info by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrderByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns orders with pagination
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Order>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrders([FromQuery] PaginationDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrdersRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Updates order
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateOrderRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}