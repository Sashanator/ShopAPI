using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopAPI.Features.Payments.RequestHandling.Requests;
using ShopAPI.Features.Payments.Services;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Payments.RequestHandling.Handlers
{
    public class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdRequest, Response>
    {
        private readonly IPaymentService _paymentService;
        public GetPaymentByIdHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Response> Handle(GetPaymentByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _paymentService.GetPaymentById(request.PaymentId, cancellationToken);
                return Response.Ok(request.Id, result);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
