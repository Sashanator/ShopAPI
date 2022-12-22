using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Payments.RequestHandling.Requests
{
    public class UpdatePaymentStatusRequest : Request<Response>
    {
        public UpdatePaymentStatusRequest(Guid paymentId, PaymentStatus status)
        {
            PaymentId = paymentId;
            Status = status;
        }
        public Guid PaymentId { get; set; }

        public PaymentStatus Status { get; set; }
    }
}
