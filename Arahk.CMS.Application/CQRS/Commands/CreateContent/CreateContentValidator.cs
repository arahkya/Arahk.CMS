using FluentValidation;

namespace Arahk.CMS.Application.CQRS.Commands.CreateContent;

public class CreateContentValidator : AbstractValidator<CreateContentRequest>
{
    public CreateContentValidator()
    {
        RuleFor(p => p.Title).NotEmpty().Length(2, 150);
        RuleFor(p => p.Message).NotEmpty().Length(2, 1500);
    }
}