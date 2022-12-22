using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Notifications.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Notifications.RequestHandling.Requests
{
    public class CreateNotificationRequest : Request<Response>
    {
        public CreateNotificationRequest(CreateNotificationDto dto)
        {
            Dto = dto;
        }
        public CreateNotificationDto Dto { get; set; }
    }
}
