using EmployeeService.WebApi;
using ShopAPI.Features.Common;
using ShopAPI.Features.Products.Model;
using ShopAPI.Features.Products.RequestHandling.Dto;

namespace ShopAPI.Features.Products.Services;

/// <summary>
///     Products service
/// </summary>
public interface IProductService
{
    /// <summary>
    ///     Creates new product
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CreateProduct(CreateProductDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Updates existing product
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateProduct(UpdateProductDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Deletes product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteProduct(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Returns product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Product> GetProductById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Returns products with pagination
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedResult<Product>> GetProducts(PaginationDto dto, CancellationToken cancellationToken);
}