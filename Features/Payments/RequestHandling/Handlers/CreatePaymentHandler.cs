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
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, Response>
    {
        private readonly IPaymentService _paymentService;
        public CreatePaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Response> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _paymentService.CreatePayment(request.Dto, cancellationToken);
                return Response.Ok(request.Id);
            }
            catch (Exception e)
            {
                return Response.InternalServerError(request.Id, e);
            }
        }
    }
}
