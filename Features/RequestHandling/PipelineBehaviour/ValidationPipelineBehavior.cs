using FluentValidation;
using MediatR;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.RequestHandling.PipelineBehaviour;

/// <inheritdoc />
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class where TRequest : MediatR.IRequest<TResponse>
{
    private readonly IValidator<TRequest> _compositeValidator;

    /// <summary>
    /// </summary>
    /// <param name="compositeValidator"></param>
    public ValidationPipelineBehavior(IValidator<TRequest> compositeValidator)
    {
        _compositeValidator = compositeValidator;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await _compositeValidator.ValidateAsync(request, cancellationToken);

        if (result.IsValid)
            return await next();

        if (!(request is Request typedRequest))
            return await next();


        var response =
            Response.BadRequest(
                typedRequest.Id,
                new ApplicationException("Validation error"),
                result.Errors
                    .DistinctBy(x => x.PropertyName)
                    .ToDictionary(kvp => kvp.PropertyName, kvp => kvp.ErrorMessage));

        return response as TResponse;
    }
}