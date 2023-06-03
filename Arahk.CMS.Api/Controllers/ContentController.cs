using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Arahk.CMS.Application.CQRS.Commands.CreateContent;
using Arahk.CMS.Application.CQRS.Queryies.ListContent;
using Arahk.CMS.Domain.CMS;

namespace Arahk.CMS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController : ControllerBase
{
    private readonly ILogger<ContentController> _logger;
    private readonly IMediator mediator;

    public ContentController(ILogger<ContentController> logger, IMediator mediator)
    {
        _logger = logger;
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateContentModel model)
    {
        CreateContentRequest request = model.Adapt<CreateContentRequest>();
        await mediator.Send(request);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        IEnumerable<Content> contentList = await mediator.Send(new ListContentRequest());

        return Ok(contentList);
    }
}
