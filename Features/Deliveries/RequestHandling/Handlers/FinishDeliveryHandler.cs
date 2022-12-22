using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopAPI.Features.Deliveries.RequestHandling.Requests;
using ShopAPI.Features.Deliveries.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Deliveries.RequestHandling.Handlers
{
    public class FinishDeliveryHandler : IRequestHandler<FinishDeliveryRequest, Response>
    {
        private readonly IDeliveryService _deliveryService;
        public FinishDeliveryHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<Response> Handle(FinishDeliveryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _deliveryService.EndDelivery(request.DeliveryId, cancellationToken);
                return Response.Ok(request.Id);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
