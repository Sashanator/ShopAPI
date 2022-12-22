using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeService.WebApi;
using ShopAPI.Features.Common;
using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Notifications.Model;
using ShopAPI.Features.Notifications.RequestHandling.Dto;

namespace ShopAPI.Features.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task SendNotification(CreateNotificationDto dto, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(dto);
            await _unitOfWork.NotificationsRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Notification> GetNotificationById(Guid notificationId, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationsRepository.GetByIdAsync(notificationId);
            return notification!;
        }

        public async Task<PagedResult<Notification>> GetNotifications(PaginationDto dto, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork.NotificationsRepository.GetPagedAsync(dto.PageSize * dto.PageIndex,
                dto.PageSize, "CreatedAt", SortDirection.Desc);
            var totalCount = await _unitOfWork.NotificationsRepository.CountAllAsync();
            return new PagedResult<Notification>
            {
                Results = notifications,
                TotalCount = totalCount
            };
        }
    }
}
