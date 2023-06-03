using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Application.Repositories;
using MediatR;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Arahk.CMS.Application.CQRS.Commands.CreateContent;

public class CreateContentHandler : IRequestHandler<CreateContentRequest, Unit>
{
    private readonly IRepository<Content> repository;
    private readonly ILogger<CreateContentHandler> logger;

    public CreateContentHandler(IRepository<Content> repository, ILogger<CreateContentHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<Unit> Handle(CreateContentRequest request, CancellationToken cancellationToken)
    {
        Content newContent = request.Adapt<Content>();

        newContent.Id = Guid.NewGuid();

        await repository.AddAsync(newContent);
        await repository.CommitChangedAsync();

        logger.LogInformation($"Entity Id {newContent.Id}");

        return Unit.Value;
    }
}