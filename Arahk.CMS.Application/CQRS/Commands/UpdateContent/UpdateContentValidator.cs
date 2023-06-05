using FluentValidation;

namespace Arahk.CMS.Application.CQRS.Commands.UpdateContent;

public class UpdateContentValidator : AbstractValidator<UpdateContentRequest>
{
    public UpdateContentValidator()
    {
        RuleFor(p => p.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(p => p.Title).NotEmpty().Length(2, 150);
        RuleFor(p => p.Message).NotEmpty().Length(2, 1500);
    }
}