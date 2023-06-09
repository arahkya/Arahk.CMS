namespace Arahk.CMS.Infrastructure.Persistants;

public class ChangedAuditEntry
{
    public Guid Id { get; set; }
    
    public Guid EntityId { get; set; } = default!;

    public string EntityName { get; set; } = default!;

    public DateTime ChangedDate { get; set; }

    public string ChangedType { get; set; } = default!;

    public Guid ChangedByUserId { get; set; }

    public string? PropertyName { get; set; }

    public string? PreviousValue { get; set; }
    
    public string? NewValue { get; set; }

    public Guid ChangedBatchId { get; set; }
}