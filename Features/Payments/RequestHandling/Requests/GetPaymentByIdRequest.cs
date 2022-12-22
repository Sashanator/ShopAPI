using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Payments.RequestHandling.Requests
{
    public class GetPaymentByIdRequest : Request<Response>
    {
        public GetPaymentByIdRequest(Guid paymentId)
        {
            PaymentId = paymentId;
        }
        public Guid PaymentId { get; set; }
    }
}
