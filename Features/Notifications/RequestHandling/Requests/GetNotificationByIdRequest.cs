using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Notifications.RequestHandling.Requests
{
    public class GetNotificationByIdRequest : Request<Response>
    {
        public GetNotificationByIdRequest(Guid notificationId)
        {
            NotificationId = notificationId;
        }
        public Guid NotificationId { get; set; }
    }
}
