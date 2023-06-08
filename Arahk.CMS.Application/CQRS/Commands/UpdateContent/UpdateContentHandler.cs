using Arahk.CMS.Application.Repositories;
using Arahk.CMS.Domain.CMS;
using MediatR;
using Mapster;

namespace Arahk.CMS.Application.CQRS.Commands.UpdateContent;

public class UpdateContentHandler : IRequestHandler<UpdateContentRequest, Unit>
{
    private readonly IRepository<Content> repository;

    public UpdateContentHandler(IRepository<Content> repository)
    {
        this.repository = repository;
    }

    public async Task<Unit> Handle(UpdateContentRequest request, CancellationToken cancellationToken)
    {
        TypeAdapterConfig updateContentMapConfig = new TypeAdapterConfig();
        updateContentMapConfig.NewConfig<UpdateContentRequest, Content>()
                              .Ignore(s => s.Id);

        Content existedContent = (await repository.GetAsync(request.Id)) ?? throw new Exception($"Content not found with Id {request.Id}");
        existedContent = request.Adapt(existedContent, updateContentMapConfig);

        await repository.CommitChangedAsync();

        return Unit.Value;
    }
}