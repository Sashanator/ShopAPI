using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPI.Features.Deliveries.Model;
using ShopAPI.Features.Deliveries.RequestHandling.Dto;

namespace ShopAPI.Features.Deliveries.Services
{
    public interface IDeliveryService
    {
        Task CreateDelivery(CreateDeliveryDto dto, CancellationToken cancellationToken);

        Task StartDelivery(Guid deliveryId, CancellationToken cancellationToken);

        Task EndDelivery(Guid deliveryId, CancellationToken cancellationToken);

        Task<Delivery> GetDeliveryById(Guid deliveryId, CancellationToken cancellationToken);
    }
}
