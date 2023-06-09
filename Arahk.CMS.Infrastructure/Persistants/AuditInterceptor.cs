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

        foreach (EntityEntry changedEntry in changeEntries.ToList())
        {
            if (changedEntry.Entity is IEntity && changedEntry.Entity is IAuditable)
            {
                string changedState = changedEntry.State switch
                {
                    EntityState.Added => "Added",
                    EntityState.Modified => "Modified",
                    EntityState.Deleted => "Delete",
                    _ => "Unknown"
                };

                string entityTypeName = changedEntry.Entity.GetType().FullName ?? "Unknown";
                DateTime changedDate = dateTimeProvider.GetCurrentDateTime();
                Guid entityId = ((IEntity)changedEntry.Entity).Id;
                Guid changedByUserId = userIdProvider.GetUserId();
                Guid ChangedBatchId = Guid.NewGuid();

                if (changedState == "Delete")
                {
                    ChangedAuditEntry changeAuditEntry = new()
                    {
                        EntityId = entityId,
                        EntityName = entityTypeName,
                        ChangedDate = changedDate,
                        ChangedType = changedState,
                        ChangedByUserId = changedByUserId,
                        ChangedBatchId = ChangedBatchId
                    };
                    eventData.Context!.Add(changeAuditEntry);
                }
                else
                {

                    foreach (PropertyEntry changedProperty in changedEntry.Properties)
                    {
                        string propertyName = changedProperty.Metadata.Name;
                        if(propertyName == "Id") continue;
                        
                        ChangedAuditEntry changeAuditEntry = new()
                        {
                            EntityId = entityId,
                            EntityName = entityTypeName,
                            ChangedDate = changedDate,
                            ChangedType = changedState,
                            ChangedByUserId = changedByUserId,
                            ChangedBatchId = ChangedBatchId,
                            PropertyName = changedProperty.Metadata.Name,
                            PreviousValue = (changedState == "Added") ? null : (changedProperty.OriginalValue?.ToString() ?? null),
                            NewValue = changedProperty.CurrentValue?.ToString() ?? null
                        };

                        eventData.Context!.Add(changeAuditEntry);
                    }
                }

                eventData.Context!.SaveChanges();
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}