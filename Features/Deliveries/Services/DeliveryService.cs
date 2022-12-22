using AutoMapper;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Deliveries.Model;
using ShopAPI.Features.Deliveries.RequestHandling.Dto;

namespace ShopAPI.Features.Deliveries.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DeliveryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDelivery(CreateDeliveryDto dto, CancellationToken cancellationToken)
        {
            var delivery = _mapper.Map<Delivery>(dto);
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(dto.OrderId);
            delivery.Status = DeliveryStatus.NotStarted;
            delivery.Order = order!;
            await _unitOfWork.DeliveryRepository.AddAsync(delivery);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task StartDelivery(Guid deliveryId, CancellationToken cancellationToken)
        {
            var delivery = await _unitOfWork.DeliveryRepository.GetByIdWithTrackingAsync(deliveryId);
            delivery.Status = DeliveryStatus.InProgress;
            await _unitOfWork.DeliveryRepository.UpdateAsync(delivery);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EndDelivery(Guid deliveryId, CancellationToken cancellationToken)
        {
            var delivery = await _unitOfWork.DeliveryRepository.GetByIdWithTrackingAsync(deliveryId);
            delivery.Status = DeliveryStatus.Arrived;
            await _unitOfWork.DeliveryRepository.UpdateAsync(delivery);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Delivery> GetDeliveryById(Guid deliveryId, CancellationToken cancellationToken)
        {
            var delivery = await _unitOfWork.DeliveryRepository.GetByIdAsync(deliveryId);
            return delivery;
        }
    }
}
