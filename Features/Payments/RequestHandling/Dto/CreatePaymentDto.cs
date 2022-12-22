using ShopAPI.Features.Payments.Model;

namespace ShopAPI.Features.Payments.RequestHandling.Dto;

public class CreatePaymentDto
{
    public PaymentStatus Status { get; set; }

    public Guid OrderId { get; set; }
}