using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopAPI.Features.Notifications.RequestHandling.Requests;
using ShopAPI.Features.Notifications.Services;
using ShopAPI.Features.RequestHandling.Base;
using Exception = System.Exception;

namespace ShopAPI.Features.Notifications.RequestHandling.Handlers
{
    public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdRequest,  Response>
    {
        private readonly INotificationService _notificationService;
        public GetNotificationByIdHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<Response> Handle(GetNotificationByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _notificationService.GetNotificationById(request.NotificationId, cancellationToken);
                return Response.Ok(request.Id, result);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
