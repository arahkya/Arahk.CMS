using Arahk.CMS.Application.CQRS.Commands.CreateContent;

namespace Arahk.CMS.Api;

public class CreateContentModel
{    
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
}