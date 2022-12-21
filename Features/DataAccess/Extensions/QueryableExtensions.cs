using System.Linq.Expressions;

namespace ShopAPI.Features.DataAccess.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    ///     Trying to skip query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="skip"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> TrySkip<T>(this IQueryable<T> query, int? skip)
    {
        return skip.HasValue ? query.Skip(skip.Value).AsQueryable() : query;
    }

    /// <summary>
    ///     Trying to take query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="take"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> TryTake<T>(this IQueryable<T> query, int? take)
    {
        return take.HasValue ? query.Take(take.Value).AsQueryable() : query;
    }

    /// <summary>
    ///     Dynamically ads order to query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sortColumn"></param>
    /// <param name="descending"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, bool descending)
    {
        // Dynamically creates a call like this: query.OrderBy(p => p.SortColumn)
        var parameter = Expression.Parameter(typeof(T), "p");

        var command = "OrderBy";

        if (descending) command = "OrderByDescending";

        //Checks if type has the sorting property ignoring case
        var property = typeof(T).GetProperties()
            .SingleOrDefault(p => p.Name.Equals(sortColumn, StringComparison.InvariantCultureIgnoreCase));

        if (property == null)
            throw new ArgumentException(
                $"Entity {typeof(T).FullName} doesn't have the property {sortColumn} for sorting");

        //Ensure correct casing for property sorting
        sortColumn = property.Name;

        // this is the part p.SortColumn
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);

        // this is the part p => p.SortColumn
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        // finally, call the "OrderBy" / "OrderByDescending" method with the order by lamba expression
        var resultExpression = Expression.Call(typeof(Queryable), command, new[] { typeof(T), property.PropertyType },
            query.Expression, Expression.Quote(orderByExpression));

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}