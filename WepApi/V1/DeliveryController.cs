using System.Net;
using System.Runtime.InteropServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.Common;
using ShopAPI.Features.Deliveries.Model;
using ShopAPI.Features.Deliveries.RequestHandling.Dto;
using ShopAPI.Features.Deliveries.RequestHandling.Requests;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.WepApi.V1;

[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
public class DeliveryController : BaseApiController
{
    private readonly IMediator _mediator;
    public DeliveryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Creates new delivery
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateDeliveryRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Starts delivery
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}/start")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> StartDelivery(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new StartDeliveryRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Finishes delivery
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}/finish")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> FinishDelivery(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new FinishDeliveryRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns delivery info by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Delivery), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetDeliveryById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDeliveryByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}