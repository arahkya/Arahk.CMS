using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arahk.CMS.Infrastructure.Persistants.Configures;

public class ChangeAuditEntityTypeConfigure : IEntityTypeConfiguration<ChangedAuditEntry>
{
    public void Configure(EntityTypeBuilder<ChangedAuditEntry> builder)
    {
        builder.HasKey(P => P.Id);
        builder.Property(p => p.EntityId).IsRequired().HasMaxLength(40);
        builder.Property(p => p.EntityName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ChangedByUserId).IsRequired().HasMaxLength(40);        
        builder.Property(p => p.ChangedType).IsRequired().HasMaxLength(20);
        builder.Property(p => p.PropertyName).HasMaxLength(50);
        builder.Property(p => p.PreviousValue).HasMaxLength(1000);
        builder.Property(p => p.NewValue).HasMaxLength(1000);
        builder.Property(p => p.ChangedBatchId).IsRequired().HasMaxLength(40);
    }
}