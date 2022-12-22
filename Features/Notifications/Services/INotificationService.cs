using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.WebApi;
using ShopAPI.Features.Common;
using ShopAPI.Features.Notifications.Model;
using ShopAPI.Features.Notifications.RequestHandling.Dto;

namespace ShopAPI.Features.Notifications.Services
{
    public interface INotificationService
    {
        Task SendNotification(CreateNotificationDto dto, CancellationToken cancellationToken);

        Task<Notification> GetNotificationById(Guid notificationId, CancellationToken cancellationToken);

        Task<PagedResult<Notification>> GetNotifications(PaginationDto dto, CancellationToken cancellationToken);
    }
}
