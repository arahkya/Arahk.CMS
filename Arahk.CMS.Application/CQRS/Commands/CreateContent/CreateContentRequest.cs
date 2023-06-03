using MediatR;

namespace Arahk.CMS.Application.CQRS.Commands.CreateContent;

public class CreateContentRequest : IRequest<Unit>
{
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
}