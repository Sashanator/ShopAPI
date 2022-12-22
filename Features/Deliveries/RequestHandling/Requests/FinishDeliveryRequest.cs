using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Deliveries.RequestHandling.Requests
{
    public class FinishDeliveryRequest : Request<Response>
    {
        public FinishDeliveryRequest(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
        public Guid DeliveryId { get; set; }
    }
}
