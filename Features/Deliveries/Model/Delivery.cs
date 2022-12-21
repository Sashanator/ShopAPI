using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.Orders.Model;

namespace ShopAPI.Features.Deliveries.Model;

public class Delivery : BaseEntity
{
    public string DestinationAddress { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DeliveryStatus Status { get; set; }

    public Guid OrderId { get; set; }

    public Order Order { get; set; }
}