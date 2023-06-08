using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Arahk.CMS.Application.CQRS.Commands.CreateContent;
using Arahk.CMS.Application.CQRS.Commands.UpdateContent;
using Arahk.CMS.Application.CQRS.Commands.DeleteContent;
using Arahk.CMS.Application.CQRS.Queryies.ListContent;
using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Api.Models.Content;

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
    [Route("list")]
    public async Task<IActionResult> ListAsync()
    {
        IEnumerable<Content> contentList = await mediator.Send(new ListContentRequest());
        IEnumerable<ContentListItemModel> items = contentList.Select(p => p.Adapt<ContentListItemModel>());

        return Ok(items);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAsync(EditContentModel model)
    {
        UpdateContentRequest request = model.Adapt<UpdateContentRequest>();
        await mediator.Send(request);

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id)
    {
        DeleteContentRequest request = new()
        {
            Id = id
        };

        await mediator.Send(request);

        return Ok();
    }
}
