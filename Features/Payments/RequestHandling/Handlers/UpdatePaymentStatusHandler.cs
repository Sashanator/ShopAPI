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
    public class UpdatePaymentStatusHandler : IRequestHandler<UpdatePaymentStatusRequest, Response>
    {
        private readonly IPaymentService _paymentService;
        public UpdatePaymentStatusHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Response> Handle(UpdatePaymentStatusRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _paymentService.CompletePayment(request.PaymentId, cancellationToken);
                return Response.Ok(request.Id);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
