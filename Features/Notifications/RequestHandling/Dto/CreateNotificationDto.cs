using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Features.Notifications.RequestHandling.Dto
{
    public class CreateNotificationDto
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string TargetEmail { get; set; }
    }
}
