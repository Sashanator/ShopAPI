using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Common;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Notifications.RequestHandling.Requests
{
    public class GetNotificationsRequest : Request<Response>
    {
        public GetNotificationsRequest(PaginationDto dto)
        {
            Dto = dto;
        }
        public PaginationDto Dto { get; set; }
    }
}
