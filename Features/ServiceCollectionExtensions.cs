using ShopAPI.Features.Deliveries.Services;
using ShopAPI.Features.Notifications.Services;
using ShopAPI.Features.Orders.Services;
using ShopAPI.Features.Payments.Services;
using ShopAPI.Features.Products.Services;

namespace ShopAPI.Features;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Add internal services 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInternalServiceCollection(this IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IDeliveryService, DeliveryService>();
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPaymentService, PaymentService>();

        return services;
    }
}