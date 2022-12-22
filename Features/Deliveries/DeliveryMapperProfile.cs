using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPI.Features.Deliveries.Model;
using ShopAPI.Features.Deliveries.RequestHandling.Dto;

namespace ShopAPI.Features.Deliveries
{
    public class DeliveryMapperProfile : Profile
    {
        public DeliveryMapperProfile()
        {
            CreateMap<CreateDeliveryDto, Delivery>();
        }
    }
}
