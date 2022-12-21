using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.Orders.Model;

namespace ShopAPI.Features.Payments.Model;

public class Payment : BaseEntity
{
    public PaymentStatus Status { get; set; }

    public Guid OrderId { get; set; }

    public Order Order { get; set; }
}