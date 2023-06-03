using Arahk.CMS.Application.Repositories;
using Arahk.CMS.Domain.CMS;
using MediatR;

namespace Arahk.CMS.Application.CQRS.Queryies.ListContent;

public class ListContentHandler : IRequestHandler<ListContentRequest, IEnumerable<Content>>
{
    private readonly IRepository<Content> repository;

    public ListContentHandler(IRepository<Content> repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Content>> Handle(ListContentRequest request, CancellationToken cancellationToken)
    {
        List<Content> contentList = await repository.ListAsync();

        return contentList;   
    }
}