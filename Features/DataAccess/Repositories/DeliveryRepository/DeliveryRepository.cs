using ShopAPI.Features.Deliveries.Model;

namespace ShopAPI.Features.DataAccess.Repositories.DeliveryRepository;

public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(ShopDbContext context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor) { }
}