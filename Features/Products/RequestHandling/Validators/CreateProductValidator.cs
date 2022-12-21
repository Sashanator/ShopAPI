using FluentValidation;
using ShopAPI.Features.Products.RequestHandling.Requests;

namespace ShopAPI.Features.Products.RequestHandling.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
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