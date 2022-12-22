using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Payments.RequestHandling.Dto;

namespace ShopAPI.Features.Payments.Services
{
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto dto, CancellationToken cancellationToken);

        Task CompletePayment(Guid paymentId, CancellationToken cancellationToken);

        Task<Payment> GetPaymentById(Guid paymentId, CancellationToken cancellationToken);
    }
}
