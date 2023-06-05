using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Arahk.CMS.Infrastructure.Persistants;

public class DefaultDbContext : DbContext, IRepository<Content>
{
    private readonly DbContextOptions options;
    private readonly AuditInterceptor auditInterceptor;

    public DbSet<Content> Contents { get; set; } = default!;

    public DbSet<ChangedAuditEntry> ChangedAuditEntries { get; set; } = default!;

    public DefaultDbContext(DbContextOptions options, AuditInterceptor auditInterceptor) : base(options)
    {
        this.options = options;
        this.auditInterceptor = auditInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(auditInterceptor);
    }

    async Task IRepository<Content>.CommitChangedAsync()
    {
        await SaveChangesAsync();
    }

    async Task IRepository<Content>.AddAsync(Content entity)
    {
        await Contents.AddAsync(entity);
    }

    async Task<List<Content>> IRepository<Content>.ListAsync()
    {
        return await Contents.OrderByDescending(p => p.Title).ToListAsync();
    }

    async Task<Content?> IRepository<Content>.GetAsync(Guid id)
    {
        return await FindAsync<Content>(id);
    }

    async Task IRepository<Content>.DeleteAsync(Guid id)
    {
        Content? toDeleteContent = await FindAsync<Content>(id);

        if (toDeleteContent == null)
        {
            return;
        }

        Contents.Remove(toDeleteContent);
    }
}