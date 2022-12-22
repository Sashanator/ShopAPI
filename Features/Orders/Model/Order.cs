using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.Products.Model;

namespace ShopAPI.Features.Orders.Model;

public class Order : BaseEntity
{
    public string CustomerName { get; set; }

    public string CustomerPhone { get; set; }

    public string CustomerEmail { get; set; }

    public double OrderSum { get; set; }

    public OrderStatus Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}