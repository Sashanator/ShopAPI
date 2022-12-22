using EmployeeService.WebApi;
using ShopAPI.Features.Common;
using ShopAPI.Features.Orders.Model;
using ShopAPI.Features.Orders.RequestHandling.Dto;

namespace ShopAPI.Features.Orders.Services;

public interface IOrderService
{
    Task CreateOrder(CreateOrderDto dto, CancellationToken cancellationToken);

    Task UpdateOrder(UpdateOrderDto dto, CancellationToken cancellationToken);

    Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken);

    Task<PagedResult<Order>> GetOrders(PaginationDto dto, CancellationToken cancellationToken);

    Task DeleteOrderById(Guid id, CancellationToken cancellationToken);
}