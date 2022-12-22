using System.Net;
using EmployeeService.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.Common;
using ShopAPI.Features.Notifications.Model;
using ShopAPI.Features.Notifications.RequestHandling.Dto;
using ShopAPI.Features.Notifications.RequestHandling.Requests;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.WepApi.V1;

[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
public class NotificationsController : BaseApiController
{
    private readonly IMediator _mediator;
    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Sends notification
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> SendNotification([FromBody] CreateNotificationDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateNotificationRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns notification info by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Notification), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetNotificationById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNotificationByIdRequest(id), cancellationToken);
        return result.AsAspNetCoreResult();
    }

    /// <summary>
    ///     Returns notifications with pagination
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Notification>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetNotifications([FromQuery] PaginationDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNotificationsRequest(dto), cancellationToken);
        return result.AsAspNetCoreResult();
    }
}