using Microsoft.EntityFrameworkCore;
using ShopAPI.Features.DataAccess.Repositories;

namespace ShopAPI.Features.DataAccess
{
    /// <summary>
    ///     DataAccess extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds db context
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddShopServiceDbContext(this IServiceCollection services)
        {
            var postgresServer = Environment.GetEnvironmentVariable("aurora_host");
            var postgresDb = Environment.GetEnvironmentVariable("aurora_db");
            var postgresUser = Environment.GetEnvironmentVariable("aurora_user");
            var postgresPassword = Environment.GetEnvironmentVariable("aurora_password");
            var postgresPort = Environment.GetEnvironmentVariable("aurora_port");
            var postgresConnectionString = string.IsNullOrEmpty(postgresServer)
                ? null
                : $"User ID={postgresUser};Password={postgresPassword};Host={postgresServer};Port={postgresPort};Database={postgresDb};Pooling=true;Maximum Pool Size=300;";
            const string defaultConnection =
                "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=ShopDataBase;Pooling=true;";

            var opts = new DbContextOptionsBuilder<ShopDbContext>();
            opts.UseNpgsql(postgresConnectionString ?? defaultConnection);
            opts.EnableSensitiveDataLogging() /*.UseLoggerFactory(MyLoggerFactory)*/;

            services.AddScoped(f => new ShopDbContext(opts.Options));

            return services;
        }

        /// <summary>
        ///     Add repos and unit of work
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddShopServiceEntityFrameworkRepositories(this IServiceCollection services)
        {
            // Add DI here for repos

            //services.AddScoped<ITestEntitiesRepository, TestEntitiesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}