using ShopAPI.Features.DataAccess.Repositories.DeliveryRepository;
using ShopAPI.Features.DataAccess.Repositories.NotificationsRepository;
using ShopAPI.Features.DataAccess.Repositories.OrdersRepository;
using ShopAPI.Features.DataAccess.Repositories.PaymentsRepository;
using ShopAPI.Features.DataAccess.Repositories.ProductsRepository;

namespace ShopAPI.Features.DataAccess.Repositories;

public interface IUnitOfWork
{
    // Add repos here
    IDeliveryRepository DeliveryRepository { get; }

    INotificationsRepository NotificationsRepository { get; }

    IOrdersRepository OrdersRepository { get; }

    IPaymentRepository PaymentsRepository { get; }

    IProductsRepository ProductRepository { get; }

    /// <summary>
    ///     Saving changes
    /// </summary>
    /// <returns></returns>
    int SaveChanges();

    /// <summary>
    ///     Saving changes
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync();
}