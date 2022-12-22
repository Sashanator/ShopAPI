using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Payments.RequestHandling.Dto;

namespace ShopAPI.Features.Payments
{
    public class PaymentMapperProfile : Profile
    {
        protected PaymentMapperProfile()
        {
            CreateMap<CreatePaymentDto, Payment>();
        }
    }
}
