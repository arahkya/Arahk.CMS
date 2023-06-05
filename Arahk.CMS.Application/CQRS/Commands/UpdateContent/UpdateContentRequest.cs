using MediatR;

namespace Arahk.CMS.Application.CQRS.Commands.UpdateContent;

public class UpdateContentRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
}