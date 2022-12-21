using ShopAPI.Features.Notifications.Model;

namespace ShopAPI.Features.DataAccess.Repositories.NotificationsRepository;

public class NotificationsRepository : GenericRepository<Notification>, INotificationsRepository
{
    public NotificationsRepository(ShopDbContext context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor) { }
}