using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace Arahk.CMS.Application.CQRS.Behaviors;

public class ValidateBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidateBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationResult? validate = validators.FirstOrDefault()?.Validate(request);

        if (validate?.IsValid ?? true)
        {
            TResponse response = await next();

            return response;
        }

        throw new FluentValidation.ValidationException(validate.Errors);
    }
}