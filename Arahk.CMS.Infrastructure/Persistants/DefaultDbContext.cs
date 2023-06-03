using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Arahk.CMS.Infrastructure.Persistants;

public class DefaultDbContext : DbContext, IRepository<Content>
{
    public DbSet<Content> Contents { get; set; } = default!;

    public DefaultDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultDbContext).Assembly);
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
}