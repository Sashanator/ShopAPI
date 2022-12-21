using FluentValidation;
using ShopAPI.Features.DataAccess.Repositories;
using ShopAPI.Features.Products.RequestHandling.Requests;

namespace ShopAPI.Features.Products.RequestHandling.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(r => r.Dto.Id)
            .MustAsync(async (id, _) => await unitOfWork.ProductRepository.ExistsAsync(id))
            .WithMessage("Product does not exist in database");

        RuleFor(r => r.Dto.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(r => r.Dto.Count)
            .GreaterThan(0)
            .WithMessage("Count must be greater than 0");

        RuleFor(r => r.Dto.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty");

        RuleFor(r => r.Dto.Description)
            .NotEmpty()
            .WithMessage("Description must not be empty");
    }
}