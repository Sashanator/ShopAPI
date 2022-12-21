using FluentValidation;
using FluentValidation.Results;

namespace ShopAPI.Features.RequestHandling.Validation.Validators;

/// <summary>
///     Composite validator
/// </summary>
/// <typeparam name="T"></typeparam>
public class CompositeValidator<T> : IValidator<T>
{
    private readonly IEnumerable<IValidator<T>> _validators;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="validators"></param>
    public CompositeValidator(IEnumerable<IValidator<T>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    ///     Cascade mode for validator
    /// </summary>
    public CascadeMode CascadeMode { get; set; }

    /// <inheritdoc />
    public ValidationResult Validate(IValidationContext context)
    {
        var validationResults = new HashSet<ValidationResult>();

        foreach (var validator in _validators)
        {
            var res = validator.Validate(context);
            validationResults.Add(res);
        }

        return new ValidationResult(validationResults.SelectMany(vr => vr.Errors));
    }

    /// <inheritdoc />
    public async Task<ValidationResult> ValidateAsync(IValidationContext context,
        CancellationToken cancellation = new CancellationToken())
    {
        IList<ValidationResult> validationResults = new List<ValidationResult>();

        foreach (var validator in _validators)
        {
            var res = await validator.ValidateAsync(context, cancellation);
            validationResults.Add(res);
        }

        return new ValidationResult(validationResults.SelectMany(vr => vr.Errors));
    }

    /// <inheritdoc />
    public IValidatorDescriptor CreateDescriptor()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool CanValidateInstancesOfType(Type type)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ValidationResult Validate(T instance)
    {
        var failures = _validators
            .Select(v => v.Validate(instance))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(f => f != null)
            .ToList();

        return new ValidationResult(failures);
    }

    /// <inheritdoc />
    public async Task<ValidationResult> ValidateAsync(T instance,
        CancellationToken cancellation = new CancellationToken())
    {
        IList<ValidationResult> validationResults = new List<ValidationResult>();

        foreach (var validator in _validators)
        {
            var res = await validator.ValidateAsync(instance, cancellation);
            validationResults.Add(res);
        }

        return new ValidationResult(validationResults.SelectMany(vr => vr.Errors));
    }
}