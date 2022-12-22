using AutoMapper;
using EmployeeService.WebApi;
using ShopAPI.Features.Common;
using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Orders.Model;
using ShopAPI.Features.Orders.RequestHandling.Dto;
using ShopAPI.Features.Products.Model;
using ShopAPI.Features.Products.RequestHandling.Dto;

namespace ShopAPI.Features.Orders.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateOrder(CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(dto);
        await _unitOfWork.OrdersRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateOrder(UpdateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrdersRepository.GetByIdWithTrackingAsync(dto.Id);
        _mapper.Map(dto, order);
        await _unitOfWork.OrdersRepository.UpdateAsync(order!);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Order> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrdersRepository.GetByIdAsync(id);
        return order!;
    }

    public async Task<PagedResult<Order>> GetOrders(PaginationDto dto, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.OrdersRepository.GetPagedAsync(dto.PageIndex * dto.PageSize, dto.PageSize,
            "CreatedAt", SortDirection.Desc);
        var totalCount = await _unitOfWork.OrdersRepository.CountAllAsync();
        return new PagedResult<Order>
        {
            Results = products,
            TotalCount = totalCount
        };
    }

    public async Task DeleteOrderById(Guid id, CancellationToken cancellationToken)
    {
        await _unitOfWork.OrdersRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddProductsToOrder(Guid orderId, List<Guid> productsIds, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrdersRepository.GetByIdWithTrackingAsync(orderId);
        var products = await _unitOfWork.ProductRepository.GetByIdsAsync(productsIds);
        foreach (var product in products)
        {
            order.Products.Add(product);
        }
        await _unitOfWork.OrdersRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }
}