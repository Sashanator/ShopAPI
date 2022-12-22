using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPI.Features.Notifications.Model;
using ShopAPI.Features.Notifications.RequestHandling.Dto;

namespace ShopAPI.Features.Notifications
{
    public class NotificationsMapperProfile : Profile
    {
        public NotificationsMapperProfile()
        {
            CreateMap<CreateNotificationDto, Notification>();
        }
    }
}
