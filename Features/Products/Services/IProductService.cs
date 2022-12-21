using ShopAPI.Features.Products.RequestHandling.Dto;

namespace ShopAPI.Features.Products.Services;

public interface IProductService
{
    Task CreateProduct(CreateProductDto dto, CancellationToken cancellationToken);
}