using ShopAPI.Features.Payments.Model;

namespace ShopAPI.Features.DataAccess.Repositories.PaymentsRepository;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ShopDbContext context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor) { }
}