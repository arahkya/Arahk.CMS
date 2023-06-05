using MediatR;

namespace Arahk.CMS.Application.CQRS.Commands.DeleteContent;

public class DeleteContentRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}