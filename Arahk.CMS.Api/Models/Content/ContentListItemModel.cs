namespace Arahk.CMS.Api.Models.Content;

public class ContentListItemModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
}