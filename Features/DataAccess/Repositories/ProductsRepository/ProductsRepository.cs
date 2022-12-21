using ShopAPI.Features.Products.Model;

namespace ShopAPI.Features.DataAccess.Repositories.ProductsRepository;

public class ProductsRepository : GenericRepository<Product>, IProductsRepository
{
    public ProductsRepository(ShopDbContext context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor) { }
}