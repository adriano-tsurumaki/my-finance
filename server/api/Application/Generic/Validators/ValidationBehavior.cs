using FluentValidation;
using MediatR;

namespace api.Application.Generic.Validators;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToList();

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        return await next(cancellationToken);
    }
}
