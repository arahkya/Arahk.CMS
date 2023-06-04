using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Arahk.CMS.Domain.Common;
using Arahk.CMS.Application.Common;

namespace Arahk.CMS.Infrastructure.Persistants;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IUserIdProvider userIdProvider;
    private readonly IDateTimeProvider dateTimeProvider;

    public AuditInterceptor(IUserIdProvider userIdProvider, IDateTimeProvider dateTimeProvider)
    {
        this.userIdProvider = userIdProvider;
        this.dateTimeProvider = dateTimeProvider;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> changeEntries = eventData.Context?.ChangeTracker.Entries() ?? Enumerable.Empty<EntityEntry>();

        foreach (EntityEntry changeEntry in changeEntries.ToList())
        {
            if (changeEntry.Entity is IEntity && changeEntry.Entity is IAuditable)
            {
                string changedState = changeEntry.State switch
                {
                    EntityState.Added => "Added",
                    EntityState.Modified => "Modified",
                    EntityState.Deleted => "Delete",
                    _ => "Unknown"
                };

                string entityTypeName = changeEntry.Entity.GetType().FullName ?? "Unknown";
                DateTime changedDate = dateTimeProvider.GetCurrentDateTime();
                Guid entityId = ((IEntity)changeEntry.Entity).Id;
                Guid changedByUserId = userIdProvider.GetUserId();

                ChangedAuditEntry changeAuditEntry = new()
                {
                    EntityId = entityId,
                    EntityName = entityTypeName,
                    ChangedDate = changedDate,
                    ChangedType = changedState,
                    ChangedByUserId = changedByUserId
                };

                eventData.Context!.Add(changeAuditEntry);
                eventData.Context!.SaveChanges();
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}