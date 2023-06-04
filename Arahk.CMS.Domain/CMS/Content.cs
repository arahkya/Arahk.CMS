using Arahk.CMS.Domain.Common;

namespace Arahk.CMS.Domain.CMS;

public class Content : IEntity, IAuditable
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
}