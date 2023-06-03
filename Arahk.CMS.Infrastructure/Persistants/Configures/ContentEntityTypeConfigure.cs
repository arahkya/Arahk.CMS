using Arahk.CMS.Domain.CMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arahk.CMS.Infrastructure.Persistants.Configures;

public class ContentEntityTypeConfigure : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Message).IsRequired().HasMaxLength(1500);
    }
}