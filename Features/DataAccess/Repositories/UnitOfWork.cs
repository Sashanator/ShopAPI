using ShopAPI.Features.DataAccess.Models;

namespace ShopAPI.Features.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShopDbContext _serviceDbContext;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="serviceDbContext"></param>
    public UnitOfWork(ShopDbContext serviceDbContext)
    {
        _serviceDbContext = serviceDbContext;

        // DI for repos (interfaces!!!)

        // TestEntitiesRepository = testEntitiesRepository;
    }

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