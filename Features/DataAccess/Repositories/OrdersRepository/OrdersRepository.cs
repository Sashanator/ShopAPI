using ShopAPI.Features.Orders.Model;

namespace ShopAPI.Features.DataAccess.Repositories.OrdersRepository;

public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
{
    public OrdersRepository(ShopDbContext context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor) { }
}