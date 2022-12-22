using AutoMapper;
using EmployeeService.WebApi;
using ShopAPI.Features.Common;
using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Products.Model;
using ShopAPI.Features.Products.RequestHandling.Dto;

namespace ShopAPI.Features.Products.Services;

/// <inheritdoc />
public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task CreateProduct(CreateProductDto dto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(dto);
        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateProduct(UpdateProductDto dto, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdWithTrackingAsync(dto.Id);
        _mapper.Map(dto, product);
        await _unitOfWork.ProductRepository.UpdateAsync(product!);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProductRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<Product> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        return product!;
    }

    /// <inheritdoc />
    public async Task<PagedResult<Product>> GetProducts(PaginationDto dto, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetPagedAsync(dto.PageIndex * dto.PageSize, dto.PageSize,
            "CreatedAt", SortDirection.Desc);
        var totalCount = await _unitOfWork.ProductRepository.CountAllAsync();
        return new PagedResult<Product>
        {
            Results = products,
            TotalCount = totalCount
        };
    }
}