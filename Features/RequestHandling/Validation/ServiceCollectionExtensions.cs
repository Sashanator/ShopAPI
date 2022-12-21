using FluentValidation;
using MediatR;
using MediatR.Registration;
using ShopAPI.Features.RequestHandling.PipelineBehaviour;
using ShopAPI.Features.RequestHandling.Validation.Validators;

namespace ShopAPI.Features.RequestHandling.Validation;

/// <summary>
///     Validation service collections
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Add dependencies for validators
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidationDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddSingleton(typeof(IValidator<>), typeof(CompositeValidator<>));
        services.AddTransient(typeof(IValidator<>), typeof(NoValidationValidator<>));
        services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .As(type => type.GetInterfaces()
                .Where(i => i.IsGenericType && !i.IsOpenGeneric() && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .AsMatchingInterface()
            .WithTransientLifetime());

        return services;
    }
}