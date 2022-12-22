using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Deliveries.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Deliveries.RequestHandling.Requests
{
    public class CreateDeliveryRequest : Request<Response>
    {
        public CreateDeliveryRequest(CreateDeliveryDto dto)
        {
            Dto = dto;
        }
        public CreateDeliveryDto Dto { get; set; }
    }
}
