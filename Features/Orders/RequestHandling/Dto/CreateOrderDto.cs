using ShopAPI.Features.Orders.Model;

namespace ShopAPI.Features.Orders.RequestHandling.Dto;

public class CreateOrderDto
{
    public string CustomerName { get; set; }

    public string CustomerPhone { get; set; }

    public string CustomerEmail { get; set; }

    public double OrderSum { get; set; }

    public OrderStatus Status { get; set; }
}