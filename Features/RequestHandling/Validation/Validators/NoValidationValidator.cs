using FluentValidation;

namespace ShopAPI.Features.RequestHandling.Validation.Validators
{
    /// <summary>
    ///     Validator for no validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NoValidationValidator<T> : AbstractValidator<T>
    {
    }
}