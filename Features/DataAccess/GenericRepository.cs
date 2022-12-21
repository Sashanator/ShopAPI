using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Features.DataAccess.Extensions;
using ShopAPI.Features.DataAccess.Models;

namespace ShopAPI.Features.DataAccess;

/// <inheritdoc />
public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
{
    /// <summary>
    ///     Context
    /// </summary>
    protected readonly ShopDbContext Context;

    protected readonly IHttpContextAccessor ContextAccessor;

    /// <summary>
    ///     User name
    /// </summary>
    protected string? UserName;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="contextAccessor"></param>
    public GenericRepository(ShopDbContext context, IHttpContextAccessor contextAccessor)
    {
        Context = context;
        ContextAccessor = contextAccessor;
        UserName = ContextAccessor.HttpContext?.User?.Identity?.Name;
    }

    /// <summary>
    ///     Set for entity
    /// </summary>
    protected virtual IQueryable<T> SetWithRelatedEntities => GetContext();

    /// <summary>
    ///     Set for entity as no tracking
    /// </summary>
    protected virtual IQueryable<T> SetWithRelatedEntitiesAsNoTracking =>
        GetContext().AsNoTracking();

    /// <inheritdoc />
    public virtual T Add(T obj)
    {
        obj.CreatedAt = DateTime.UtcNow;
        var result = Context.Add(obj);
        return result.Entity;
    }

    /// <inheritdoc />
    public virtual async Task<T> AddAsync(T obj)
    {
        obj.CreatedAt = DateTime.UtcNow; ;
        var result = await Context.AddAsync(obj);
        return result.Entity;
    }

    /// <inheritdoc />
    public virtual List<T> AddMany(IEnumerable<T> objs)
    {
        if (objs == null) throw new ArgumentNullException(nameof(objs));
        var baseEntities = objs.ToList();

        foreach (var o in baseEntities)
        {
            o.CreatedAt = DateTime.UtcNow;
        }

        Context.AddRange(baseEntities);
        return baseEntities;
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> AddManyAsync(IEnumerable<T> objs)
    {
        if (objs == null) throw new ArgumentNullException(nameof(objs));
        var baseEntities = objs.ToList();

        foreach (var o in baseEntities)
        {
            o.CreatedAt = DateTime.UtcNow;
        }

        await Context.AddRangeAsync(baseEntities);
        return baseEntities;
    }

    public virtual void AttachMany(IEnumerable<T> objs)
    {
        Context.AttachRange(objs);
    }

    /// <inheritdoc />
    public virtual long Count(Expression<Func<T, bool>> predicate)
    {
        return GetContext().Where(predicate).LongCount();
    }


    /// <inheritdoc />
    public virtual long CountAll()
    {
        return GetContext().LongCount();
    }

    /// <inheritdoc />
    public virtual async Task<long> CountAllAsync()
    {
        return await GetContext().LongCountAsync();
    }

    /// <inheritdoc />
    public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await SetWithRelatedEntitiesAsNoTracking.Where(predicate).LongCountAsync();
    }

    /// <inheritdoc />
    public virtual void Delete(Guid id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            Update(entity);
        }
    }

    /// <inheritdoc />
    public virtual void Delete(T? obj)
    {
        if (obj != null)
        {
            obj.IsDeleted = true;
            Update(obj);
        }
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(T? obj)
    {
        if (obj != null)
        {
            obj.IsDeleted = true;
            await UpdateAsync(obj);
        }
    }

    /// <inheritdoc />
    public virtual void DeleteMany(IEnumerable<Guid> ids)
    {
        var entities = GetByIds(ids);
        foreach (var baseEntity in entities)
        {
            baseEntity.IsDeleted = true;
            Update(baseEntity);
        }
    }

    /// <inheritdoc />
    public virtual void DeleteMany(IEnumerable<T> objs)
    {
        foreach (var baseEntity in objs)
        {
            baseEntity.IsDeleted = true;
            Update(baseEntity);
        }
    }

    /// <inheritdoc />
    public virtual async Task DeleteManyAsync(IEnumerable<Guid> ids)
    {
        var entities = await GetByIdsAsync(ids);
        foreach (var baseEntity in entities)
        {
            baseEntity.IsDeleted = true;
            await UpdateAsync(baseEntity);
        }
    }

    /// <inheritdoc />
    public virtual async Task DeleteManyAsync(IEnumerable<T> objs)
    {
        foreach (var baseEntity in objs)
        {
            baseEntity.IsDeleted = true;
            await UpdateAsync(baseEntity);
        }
    }

    /// <inheritdoc />
    public virtual bool Exists(Guid id)
    {
        return SetWithRelatedEntitiesAsNoTracking.Any(e => e.Id == id);
    }

    /// <inheritdoc />
    public virtual Task<bool> ExistsAsync(Guid id)
    {
        return SetWithRelatedEntitiesAsNoTracking.AnyAsync(e => e.Id == id);
    }

    /// <inheritdoc />
    public virtual List<T> GetAll(List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return context.ToList();
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> GetAllAsync(List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return await context.ToListAsync();
    }

    /// <inheritdoc />
    public virtual T? GetById(Guid id, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return context.SingleOrDefault(e => e.Id == id);
    }

    /// <inheritdoc />
    public T? GetByIdWithTracking(Guid id, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return context.SingleOrDefault(e => e.Id == id);
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync(Guid id, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return await context.SingleOrDefaultAsync(e => e.Id == id);
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdWithTrackingAsync(Guid id, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return await context.SingleOrDefaultAsync(e => e.Id == id);
    }

    /// <inheritdoc />
    public virtual T? SingleOrDefault(Expression<Func<T, bool>> predicate, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return context.SingleOrDefault(predicate);
    }

    /// <inheritdoc />
    public T? SingleOrDefaultWithTracking(Expression<Func<T, bool>> predicate, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return context.SingleOrDefault(predicate);
    }

    /// <inheritdoc />
    public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,
        List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return await context.SingleOrDefaultAsync(predicate);
    }

    /// <inheritdoc />
    public async Task<T?> SingleOrDefaultWithTrackingAsync(Expression<Func<T, bool>> predicate,
        List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return await context.SingleOrDefaultAsync(predicate);
    }

    /// <inheritdoc />
    public virtual T? FirstOrDefault(Expression<Func<T, bool>> predicate, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return context.FirstOrDefault(predicate);
    }

    public virtual T? LastOrDefault(Expression<Func<T, bool>> predicate, string? sortBy = null,
        List<string>? relations = null)
    {
        return SearchQuery(predicate, sortBy, SortDirection.Asc, relations: relations).LastOrDefault();
    }

    /// <inheritdoc />
    public virtual T? FirstOrDefaultWithTracking(Expression<Func<T, bool>> predicate, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return context.FirstOrDefault(predicate);
    }

    public virtual T? LastOrDefaultWithTracking(Expression<Func<T, bool>> predicate, string? sortBy = null,
        List<string>? relations = null)
    {
        return SearchQueryWithTracking(predicate, sortBy, SortDirection.Asc, relations: relations).LastOrDefault();
    }

    /// <inheritdoc />
    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
        List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return await context.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<T?> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        List<string>? relations = null)
    {
        return await SearchQuery(predicate, sortBy, SortDirection.Asc, relations: relations).LastOrDefaultAsync();
    }

    /// <inheritdoc />
    public virtual async Task<T?> FirstOrDefaultWithTrackingAsync(Expression<Func<T, bool>> predicate,
        List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return await context.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<T?> LastOrDefaultWithTrackingAsync(Expression<Func<T, bool>> predicate,
        string? sortBy = null, List<string>? relations = null)
    {
        return await SearchQueryWithTracking(predicate, sortBy, SortDirection.Asc, relations: relations)
            .LastOrDefaultAsync();
    }

    /// <inheritdoc />
    public virtual List<T> GetByIds(IEnumerable<Guid> ids, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return context.Where(e => ids.Contains(e.Id)).ToList();
    }

    /// <inheritdoc />
    public List<T> GetByIdsWithTracking(IEnumerable<Guid> ids, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return context.Where(e => ids.Contains(e.Id)).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> GetByIdsAsync(IEnumerable<Guid> ids, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        return await context.Where(e => ids.Contains(e.Id)).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<T>> GetByIdsWithTrackingAsync(IEnumerable<Guid> ids, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;
        return await SetWithRelatedEntities.Where(e => ids.Contains(e.Id)).ToListAsync();
    }

    /// <inheritdoc />
    public virtual List<T> GetPaged(int? skip, int? take, string? sortBy = null,
        SortDirection? sortDirection = null, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        var query = context.TrySkip(skip).TryTake(take);

        if (!string.IsNullOrEmpty(sortBy))
            query = query.OrderByDynamic(sortBy, (sortDirection ?? SortDirection.Asc) == SortDirection.Desc);

        return query.ToList();
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> GetPagedAsync(int? skip, int? take, string? sortBy = null,
        SortDirection? sortDirection = null, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;
        var query = context
            .OrderByDynamic(sortBy ?? "Id", (sortDirection ?? SortDirection.Asc) == SortDirection.Desc)
            .TrySkip(skip)
            .TryTake(take);

        return await query.ToListAsync();
    }

    /// <inheritdoc />
    public virtual List<T> Search(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null,
        int? take = null, List<string>? relations = null)
    {
        return SearchQuery(predicate, sortBy, sortDirection, skip, take, relations).ToList();
    }

  

    /// <inheritdoc />
    public List<T> SearchWithTracking(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null)
    {
        return SearchQueryWithTracking(predicate, sortBy, sortDirection, skip, take, relations).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> SearchAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null,
        int? take = null, List<string>? relations = null)
    {
        return await SearchQuery(predicate, sortBy, sortDirection, skip, take, relations).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<T>> SearchWithTrackingAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null)
    {
        return await SearchQueryWithTracking(predicate, sortBy, sortDirection, skip, take, relations).ToListAsync();
    }

    /// <inheritdoc />
    public virtual List<Guid> SearchIds(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null,
        int? take = null, List<string>? relations = null)
    {
        return SearchQuery(predicate, sortBy, sortDirection, skip, take, relations).Select(e => e.Id).ToList();
    }

    /// <inheritdoc />
    public virtual async Task<List<Guid>> SearchIdsAsync(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null, int? skip = null,
        int? take = null, List<string>? relations = null)
    {
        return await SearchQuery(predicate, sortBy, sortDirection, skip, take, relations).Select(e => e.Id)
            .ToListAsync();
    }

    /// <inheritdoc />
    public virtual T Update(T obj)
    {
        obj.LastModifiedAt = DateTime.UtcNow;

        try
        {
            Context.Entry(obj).State = EntityState.Modified;
        }
        catch (InvalidOperationException ex)
        {
            //If the entity is already being tracked, just ignore this error and let execution continue
            if (!ex.Message.Contains("already being tracked")) throw;
        }

        return obj;
    }

    /// <inheritdoc />
    public virtual async Task<T> UpdateAsync(T obj)
    {
        obj.LastModifiedAt = DateTime.UtcNow;

        try
        {
            Context.Entry(obj).State = EntityState.Modified;
        }
        catch (InvalidOperationException ex)
        {
            //If the entity is already being tracked, just ignore this error and let execution continue
            if (!ex.Message.Contains("already being tracked")) throw;
        }

        return await Task.FromResult(obj);
    }

    /// <inheritdoc />
    public virtual void UpdateRange(IEnumerable<T> objs)
    {
        foreach (var obj in objs)
        {
            obj.LastModifiedAt = DateTime.UtcNow;

            try
            {
                Context.Entry(obj).State = EntityState.Modified;
            }
            catch (InvalidOperationException ex)
            {
                //If the entity is already being tracked, just ignore this error and let execution continue
                if (!ex.Message.Contains("already being tracked")) throw;
            }
        }
    }

    public virtual IQueryable<T> SearchQuery(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetNoTrackingContextWithRelations(relations)
            : SetWithRelatedEntitiesAsNoTracking;

        if (string.IsNullOrEmpty(sortBy))
            return context.Where(predicate).TrySkip(skip ?? 0)
                .TryTake(take ?? int.MaxValue);

        //Checks if type has the sorting property ignoring case
        var propertyInfo = typeof(T).GetProperties().SingleOrDefault(p => p.Name.ToLower() == sortBy.ToLower());

        if (propertyInfo == null)
            throw new ArgumentException(
                $"Entity {typeof(T).FullName} doesn't have the property {sortBy} for sorting");

        //Ensure correct casing for property sorting
        sortBy = propertyInfo.Name;

        return context.Where(predicate)
            .OrderByDynamic(sortBy, (sortDirection ?? SortDirection.Asc) == SortDirection.Desc).TrySkip(skip)
            .TryTake(take);
    }

    public virtual IQueryable<T> SearchQueryWithTracking(Expression<Func<T, bool>> predicate, string? sortBy = null,
        SortDirection? sortDirection = null,
        int? skip = null, int? take = null, List<string>? relations = null)
    {
        var context = relations != null && RelationsAreValid(relations)
            ? GetTrackingContextWithRelations(relations)
            : SetWithRelatedEntities;

        if (string.IsNullOrEmpty(sortBy))
            return context.Where(predicate).TrySkip(skip ?? 0)
                .TryTake(take ?? int.MaxValue);

        var propertyInfo = typeof(T).GetProperties().SingleOrDefault(p => p.Name.ToLower() == sortBy.ToLower());

        if (propertyInfo == null)
            throw new ArgumentException(
                $"Entity {typeof(T).FullName} doesn't have the property {sortBy} for sorting");

        sortBy = propertyInfo.Name;

        return context.Where(predicate)
            .OrderByDynamic(sortBy, (sortDirection ?? SortDirection.Asc) == SortDirection.Desc).TrySkip(skip)
            .TryTake(take);
    }

    /// <summary>
    ///     Returns context with client group filtration and not deleted entities
    /// </summary>
    /// <returns></returns>
    protected IQueryable<T> GetContext()
    {
        var context = Context.Set<T>().Where(c => !c.IsDeleted);
        return context;
    }

    private IQueryable<T> GetNoTrackingContextWithRelations(IEnumerable<string> relations)
    {
        var context = SetWithRelatedEntitiesAsNoTracking;
        context = relations.Aggregate(context, (current, relation) => current.Include(relation));
        return context;
    }

    private IQueryable<T> GetTrackingContextWithRelations(IEnumerable<string> relations)
    {
        var context = SetWithRelatedEntities;
        context = relations.Aggregate(context, (current, relation) => current.Include(relation));
        return context;
    }

    private static bool RelationsAreValid(List<string> relations)
    {
        foreach (var relation in relations) TypeHasRelationProperty(typeof(T), relation);
        return true;
    }

    private static bool TypeHasRelationProperty(Type? type, string relation)
    {
        if (relation.Contains('.'))
        {
            var propertyInfo = type?.GetProperty(relation.Split('.')[0]);
            if (propertyInfo == null)
                throw new MissingFieldException($"This entity does not have such field: {relation}");

            var regexInsideSquareBrackets = new Regex(@"\[(.*?)\]");
            var propertyTypeFullPath = propertyInfo.ToString()!.Contains('[')
                ? regexInsideSquareBrackets.Match(propertyInfo.ToString()!).Groups[1].ToString()
                : propertyInfo.ToString()!.Split(' ')[0].Trim('{');

            var entryAssembly = type?.Assembly?.GetName()?.Name;
            if (entryAssembly == null) throw new Exception("Entry assembly is null");

            var newType = Type.GetType($"{propertyTypeFullPath}, {entryAssembly}");
            return TypeHasRelationProperty(newType, relation[(relation.IndexOf('.') + 1)..]);
        }

        if (type?.GetProperty(relation) == null)
            throw new MissingFieldException($"This entity does not have such field: {relation}");
        return true;
    }
}