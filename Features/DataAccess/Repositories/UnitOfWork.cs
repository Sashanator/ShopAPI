using ShopAPI.Features.DataAccess.Repositories.DeliveryRepository;
using ShopAPI.Features.DataAccess.Repositories.NotificationsRepository;
using ShopAPI.Features.DataAccess.Repositories.OrdersRepository;
using ShopAPI.Features.DataAccess.Repositories.PaymentsRepository;
using ShopAPI.Features.DataAccess.Repositories.ProductsRepository;

namespace ShopAPI.Features.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopDbContext _serviceDbContext;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="serviceDbContext"></param>
    /// <param name="deliveryRepository"></param>
    /// <param name="notificationsRepository"></param>
    /// <param name="ordersRepository"></param>
    /// <param name="paymentsRepository"></param>
    /// <param name="productRepository"></param>
    public UnitOfWork(
        ShopDbContext serviceDbContext, 
        IDeliveryRepository deliveryRepository, 
        INotificationsRepository notificationsRepository, 
        IOrdersRepository ordersRepository, 
        IPaymentRepository paymentsRepository, 
        IProductsRepository productRepository)
    {
        _serviceDbContext = serviceDbContext;
        DeliveryRepository = deliveryRepository;
        NotificationsRepository = notificationsRepository;
        OrdersRepository = ordersRepository;
        PaymentsRepository = paymentsRepository;
        ProductRepository = productRepository;

        // TestEntitiesRepository = testEntitiesRepository;
    }

    public IDeliveryRepository DeliveryRepository { get; }
    public INotificationsRepository NotificationsRepository { get; }
    public IOrdersRepository OrdersRepository { get; }
    public IPaymentRepository PaymentsRepository { get; }
    public IProductsRepository ProductRepository { get; }

    public int SaveChanges()
    {
        return _serviceDbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync()
    {
        return await _serviceDbContext.SaveChangesAsync();
    }
}