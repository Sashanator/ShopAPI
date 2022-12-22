using AutoMapper;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Payments.RequestHandling.Dto;

namespace ShopAPI.Features.Payments
{
    public class PaymentMapperProfile : Profile
    {
        public PaymentMapperProfile()
        {
            CreateMap<CreatePaymentDto, Payment>();
        }
    }
}
