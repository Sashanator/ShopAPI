using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShopAPI.Features.DataAccess;

/// <summary>
/// Used to create a DbContext when creating and applying migrations via command line on the developer's machine
/// </summary>
public class ShopDbContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public ShopDbContext CreateDbContext(string[] args)
    {
        var postgresServer = Environment.GetEnvironmentVariable("aurora_host");
        var postgresDb = Environment.GetEnvironmentVariable("aurora_db");
        var postgresUser = Environment.GetEnvironmentVariable("aurora_user");
        var postgresPassword = Environment.GetEnvironmentVariable("aurora_password");
        var postgresPort = Environment.GetEnvironmentVariable("aurora_port");
        var postgresConnectionString = string.IsNullOrEmpty(postgresServer) ? null : $"User ID={postgresUser};Password={postgresPassword};Host={postgresServer};Port={postgresPort};Database={postgresDb};Pooling=true;Maximum Pool Size=300;";
        const string defaultConnection =
            "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=ShopDataBase;Pooling=true;";

        var opts = new DbContextOptionsBuilder<ShopDbContext>();
        opts.EnableSensitiveDataLogging(true);
        opts.UseLoggerFactory(MyLoggerFactory);
        opts.UseNpgsql(postgresConnectionString ?? defaultConnection);

        return new ShopDbContext(opts.Options);
    }
}