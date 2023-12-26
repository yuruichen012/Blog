using IdentityManagement.Infrastructure.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Infrastructure;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

public class Repository<TKey, TEntity>(IMediator mediator, IdentityManagementDbContext dbContext) : EfCoreRepository<TKey, TEntity, IdentityManagementDbContext>(dbContext) where TEntity : Entity<TKey>, IAggregateRoot
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 获取存储事件的实体
        var entries = DbContext.ChangeTracker.Entries<IAggregateRoot>().ToList();

        // 领域事件发布
        foreach (var entry in entries)
        {
            var domainEvents = entry.Entity.DomainEvents?.ToList();
            if (domainEvents == null)
            {
                continue;
            }
            entry.Entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }

        // 获取要保存的实体
        var savingEntries = entries.Where(x => x.State == EntityState.Added).ToList();

        // 聚合根保存中发布
        foreach (var entry in savingEntries)
        {
            await mediator.Publish(AggregateRootSavingEvent.GetNotificationFrom(entry.Entity), cancellationToken);
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        // 聚合根保存发布
        foreach (var entry in savingEntries)
        {
            await mediator.Publish(AggregateRootSavedEvent.GetNotificationFrom(entry.Entity), cancellationToken);
        }

        return result;
    }
}
