using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.Orders.Model;

namespace ShopAPI.Features.Products.Model;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public long Count { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}