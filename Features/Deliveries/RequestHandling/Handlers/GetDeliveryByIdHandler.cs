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
    public class GetDeliveryByIdHandler : IRequestHandler<GetDeliveryByIdRequest, Response>
    {
        private readonly IDeliveryService _deliveryService;
        public GetDeliveryByIdHandler(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<Response> Handle(GetDeliveryByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _deliveryService.GetDeliveryById(request.DeliveryId, cancellationToken);
                return Response.Ok(request.Id, result);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
