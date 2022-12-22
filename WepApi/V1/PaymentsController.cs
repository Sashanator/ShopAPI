using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.Common;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Payments.RequestHandling.Dto;
using ShopAPI.Features.Payments.RequestHandling.Requests;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.WepApi.V1;

[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
public class PaymentsController : BaseApiController
{
    private readonly IMediator _mediator;
    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Creates new payment
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreatePaymentRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns payment's info by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Payment), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPaymentById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPaymentByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Completes payment
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/update-status")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CompletePayment(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdatePaymentStatusRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}