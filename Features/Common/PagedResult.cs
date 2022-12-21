using System.Collections.Generic;

namespace EmployeeService.WebApi;

/// <summary>
///     Paginated result
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResult<T>
{
    /// <summary>
    ///     Result list page
    /// </summary>
    public List<T> Results { get; set; }

    /// <summary>
    ///     Total count of records
    /// </summary>
    public long TotalCount { get; set; }
}