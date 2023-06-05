using Arahk.CMS.Application.Repositories;
using Arahk.CMS.Domain.CMS;
using MediatR;

namespace Arahk.CMS.Application.CQRS.Commands.DeleteContent;

public class DeleteContentHandler : IRequestHandler<DeleteContentRequest, Unit>
{
    private readonly IRepository<Content> repository;

    public DeleteContentHandler(IRepository<Content> repository)
    {
        this.repository = repository;
    }

    public async Task<Unit> Handle(DeleteContentRequest request, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(request.Id);
        await repository.CommitChangedAsync();

        return Unit.Value;
    }
}