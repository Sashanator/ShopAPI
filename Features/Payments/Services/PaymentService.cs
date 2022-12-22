using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Orders.Model;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Payments.RequestHandling.Dto;

namespace ShopAPI.Features.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private delegate Task OrderHandler(Guid orderId);

        private event OrderHandler? Notify;

        public PaymentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            Notify += StartOrder;
        }

        private async Task StartOrder(Guid orderId)
        {
            var order = await _unitOfWork.OrdersRepository.GetByIdWithTrackingAsync(orderId);
            order.Status = OrderStatus.InProgress;
            await _unitOfWork.OrdersRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreatePayment(CreatePaymentDto dto, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<Payment>(dto);
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(dto.OrderId);
            payment.Status = PaymentStatus.NotPaid;
            payment.Order = order!;
            await _unitOfWork.PaymentsRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CompletePayment(Guid paymentId, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.PaymentsRepository.GetByIdWithTrackingAsync(paymentId);
            payment!.Status = PaymentStatus.Paid;
            await _unitOfWork.PaymentsRepository.UpdateAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            Notify?.Invoke(payment.OrderId);
        }

        public async Task<Payment> GetPaymentById(Guid paymentId, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.PaymentsRepository.GetByIdAsync(paymentId);
            return payment!;
        }
    }
}
