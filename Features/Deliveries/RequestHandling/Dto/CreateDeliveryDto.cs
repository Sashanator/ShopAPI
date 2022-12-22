using ShopAPI.Features.Deliveries.Model;

namespace ShopAPI.Features.Deliveries.RequestHandling.Dto;

public class CreateDeliveryDto
{
    public string DestinationAddress { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DeliveryStatus Status { get; set; }

    public Guid OrderId { get; set; }
}