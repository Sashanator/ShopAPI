using AutoMapper;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Products.Model;
using ShopAPI.Features.Products.RequestHandling.Dto;

namespace ShopAPI.Features.Products.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateProduct(CreateProductDto dto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(dto);
        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }
}