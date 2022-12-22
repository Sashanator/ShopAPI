using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Payments.RequestHandling.Dto;

namespace ShopAPI.Features.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreatePayment(CreatePaymentDto dto, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<Payment>(dto);
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(dto.OrderId);
            payment.Order = order!;
            await _unitOfWork.PaymentsRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePaymentStatus(Guid paymentId, PaymentStatus status)
        {
            var payment = await _unitOfWork.PaymentsRepository.GetByIdWithTrackingAsync(paymentId);
            payment!.Status = status;
            await _unitOfWork.PaymentsRepository.UpdateAsync(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentById(Guid paymentId, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.PaymentsRepository.GetByIdAsync(paymentId);
            return payment!;
        }
    }
}
