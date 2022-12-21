namespace ShopAPI.Features.DataAccess.Repositories;

public interface IUnitOfWork
{
    // Add repos here

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