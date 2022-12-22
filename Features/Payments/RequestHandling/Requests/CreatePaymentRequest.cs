using ShopAPI.Features.Payments.RequestHandling.Dto;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Payments.RequestHandling.Requests;

public class CreatePaymentRequest : Request<Response>
{
    public CreatePaymentRequest(CreatePaymentDto dto)
    {
        Dto = dto;
    }
    public CreatePaymentDto Dto { get; set; }
}