namespace ShopAPI.Features.Common;

public class PaginationDto
{
    /// <summary>
    ///     Current page index.
    /// </summary>
    public int PageIndex { get; set; } = 0;

    /// <summary>
    ///     Count of records on page.
    /// </summary>
    public int PageSize { get; set; } = 25;
}