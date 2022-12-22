using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Deliveries.RequestHandling.Requests
{
    public class GetDeliveryByIdRequest : Request<Response>
    {
        public GetDeliveryByIdRequest(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
        public Guid DeliveryId { get; set; }
    }
}
