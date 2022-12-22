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
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationRequest, Response>
    {
        private readonly INotificationService _notificationService;
        public CreateNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<Response> Handle(CreateNotificationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _notificationService.SendNotification(request.Dto, cancellationToken);
                return Response.Ok(request.Id);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
