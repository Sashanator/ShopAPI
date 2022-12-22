using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Deliveries.RequestHandling.Requests
{
    public class StartDeliveryRequest : Request<Response>
    {
        public StartDeliveryRequest(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
        public Guid DeliveryId { get; set; }
    }
}
