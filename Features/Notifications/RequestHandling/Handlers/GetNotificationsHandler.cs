using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopAPI.Features.Notifications.RequestHandling.Requests;
using ShopAPI.Features.Notifications.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Notifications.RequestHandling.Handlers
{
    public class GetNotificationsHandler : IRequestHandler<GetNotificationsRequest, Response>
    {
        private readonly INotificationService _notificationService;
        public GetNotificationsHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<Response> Handle(GetNotificationsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notificationService.GetNotifications(request.Dto, cancellationToken);
                return Response.Ok(request.Id, result);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
