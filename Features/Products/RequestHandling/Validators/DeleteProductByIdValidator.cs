using FluentValidation;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Products.RequestHandling.Requests;

namespace ShopAPI.Features.Products.RequestHandling.Validators;

/// <inheritdoc />
public class DeleteProductByIdValidator : AbstractValidator<DeleteProductByIdRequest>
{
    public DeleteProductByIdValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(r => r.ProductId)
            .MustAsync(async (id, _) => await unitOfWork.ProductRepository.ExistsAsync(id))
            .WithMessage("Product does not exist in database");
    }
}