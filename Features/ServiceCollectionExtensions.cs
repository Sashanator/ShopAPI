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

        return services;
    }
}