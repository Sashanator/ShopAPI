using System.Linq.Expressions;
using ShopAPI.Features.DataAccess.Models;

namespace ShopAPI.Features.DataAccess;

public interface IGenericRepository<T> where T : class, IBaseEntity
{
    /// <summary>
    ///     Add new entity
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    T Add(T obj);

    /// <summary>
    ///     Add new entity
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task<T> AddAsync(T obj);

    /// <summary>
    ///     Add many entities
    /// </summary>
    /// <param name="objs"></param>
    /// <returns></returns>
    List<T> AddMany(IEnumerable<T> objs);

    /// <summary>
    ///     Add many entities
    /// </summary>
    /// <param name="objs"></param>
    /// <returns></returns>
    Task<List<T>> AddManyAsync(IEnumerable<T> objs);

    /// <summary>
    ///     Attach many entities
    /// </summary>
    /// <param name="objs"></param>
    void AttachMany(IEnumerable<T> objs);

    /// <summary>
    ///     Count filtered by expression
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    long Count(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Count all entities
    /// </summary>
    /// <returns></returns>
    long CountAll();

    /// <summary>
    ///     Count all entities
    /// </summary>
    /// <returns></returns>
    Task<long> CountAllAsync();

    /// <summary>
    ///     Count filtered by expression
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<long> CountAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Delete entity by id
    /// </summary>
    /// <param name="id"></param>
    void Delete(Guid id);

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="obj"></param>
    void Delete(T obj);

    /// <summary>
    ///     Delete entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task DeleteAsync(T obj);

    /// <summary>
    ///     Delete entities by ids
    /// </summary>
    /// <param name="ids"></param>
    void DeleteMany(IEnumerable<Guid> ids);

    /// <summary>
    ///     Delete entities
    /// </summary>
    /// <param name="objs"></param>
    void DeleteMany(IEnumerable<T> objs);

    /// <summary>
    ///     Delete entities by ids
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task DeleteManyAsync(IEnumerable<Guid> ids);

    /// <summary>
    ///     Delete entities
    /// </summary>
    /// <param name="objs"></param>
    /// <returns></returns>
    Task DeleteManyAsync(IEnumerable<T> objs);

    /// <summary>
    ///     Check if entity exist in database by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Exists(Guid id);

    /// <summary>
    ///     Check if entity exist in database by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Guid id);

    /// <summary>
    ///     Search first or default.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? FirstOrDefault(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Async search first or default.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Search first or default with tracking.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? FirstOrDefaultWithTracking(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Async search first or default with tracking.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> FirstOrDefaultWithTrackingAsync(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Return all entities from database
    /// </summary>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<T> GetAll(List<string>? relations = null);

    /// <summary>
    ///     Return all entities from database
    /// </summary>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<T>> GetAllAsync(List<string>? relations = null);

    /// <summary>
    ///     Return entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? GetById(Guid id, List<string>? relations = null);

    /// <summary>
    ///     Return entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(Guid id, List<string>? relations = null);

    /// <summary>
    ///     Get entities by ids
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<T> GetByIds(IEnumerable<Guid> ids, List<string>? relations = null);

    /// <summary>
    ///     Get entities by ids
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<T>> GetByIdsAsync(IEnumerable<Guid> ids, List<string>? relations = null);

    /// <summary>
    ///     Returns entities by IDs with tracking
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<T> GetByIdsWithTracking(IEnumerable<Guid> ids, List<string>? relations = null);

    /// <summary>
    ///     Get entities by ids with tracking
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<T>> GetByIdsWithTrackingAsync(IEnumerable<Guid> ids, List<string>? relations = null);

    /// <summary>
    ///     Returns entity by ID with tracking
    /// </summary>
    /// <param name="id"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? GetByIdWithTracking(Guid id, List<string>? relations = null);

    /// <summary>
    ///     Returns entity by id with tracking
    /// </summary>
    /// <param name="id"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> GetByIdWithTrackingAsync(Guid id, List<string>? relations = null);

    /// <summary>
    ///     Get paged entities
    /// </summary>
    /// <param name="skip">how much to skip</param>
    /// <param name="take">how much to take</param>
    /// <param name="sortBy">sorting</param>
    /// <param name="sortDirection">sort direction</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<T> GetPaged(int? skip, int? take, string? sortBy = null, SortDirection? sortDirection = null,
        List<string>? relations = null);

    /// <summary>
    ///     Get paged entities
    /// </summary>
    /// <param name="skip">how much to skip</param>
    /// <param name="take">how much to take</param>
    /// <param name="sortBy">sorting</param>
    /// <param name="sortDirection">sort direction</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<T>> GetPagedAsync(int? skip, int? take, string? sortBy = null, SortDirection? sortDirection = null,
        List<string>? relations = null);

    /// <summary>
    ///     Search last or default.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? LastOrDefault(Expression<Func<T, bool>> predicate, string? sortBy = null, List<string>? relations = null);

    /// <summary>
    ///     Async search last or default.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        List<string>? relations = null);

    /// <summary>
    ///     Search last or default with tracking.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? LastOrDefaultWithTracking(Expression<Func<T, bool>> predicate, string? sortBy = null,
        List<string>? relations = null);

    /// <summary>
    ///     Async search last or default with tracking.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> LastOrDefaultWithTrackingAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        List<string>? relations = null);

    /// <summary>
    ///     Search paged entities
    /// </summary>
    /// <param name="skip">how much to skip</param>
    /// <param name="take">how much to take</param>
    /// <param name="predicate">expression</param>
    /// <param name="sortBy">sorting</param>
    /// <param name="sortDirection">sort direction</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<T> Search(Expression<Func<T, bool>> predicate, string? sortBy = null, SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Search paged entities
    /// </summary>
    /// <param name="skip">how much to skip</param>
    /// <param name="take">how much to take</param>
    /// <param name="predicate">expression</param>
    /// <param name="sortBy">sorting</param>
    /// <param name="sortDirection">sort direction</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<T>> SearchAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Search paged ids
    /// </summary>
    /// <param name="predicate">expression</param>
    /// <param name="sortBy">sorting</param>
    /// <param name="sortDirection">sort direction</param>
    /// <param name="skip">how much to skip</param>
    /// <param name="take">how much to take</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<Guid> SearchIds(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Search paged ids
    /// </summary>
    /// <param name="predicate">expression</param>
    /// <param name="sortBy">sorting</param>
    /// <param name="sortDirection">sort direction</param>
    /// <param name="skip">how much to skip</param>
    /// <param name="take">how much to take</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<Guid>> SearchIdsAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Same as Search but returns IQueryable
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    IQueryable<T> SearchQuery(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Same as Search but returns IQueryable
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    IQueryable<T> SearchQueryWithTracking(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Search entities with tracking
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    List<T> SearchWithTracking(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Async search entities with tracking
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDirection"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<List<T>> SearchWithTrackingAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null, int? take = null, List<string>? relations = null);

    /// <summary>
    ///     Search single or default.
    /// </summary>
    /// <param name="predicate">expression</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? SingleOrDefault(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Async search single or default.
    /// </summary>
    /// <param name="predicate">expression</param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Searches single or default with tracking
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    T? SingleOrDefaultWithTracking(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Async search single or default with tracking
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relations"></param>
    /// <returns></returns>
    Task<T?> SingleOrDefaultWithTrackingAsync(Expression<Func<T, bool>> predicate, List<string>? relations = null);

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    T Update(T obj);

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T obj);

    /// <summary>
    ///     Update entities.
    /// </summary>
    /// <param name="objs"></param>
    /// <returns></returns>
    void UpdateRange(IEnumerable<T> objs);
}