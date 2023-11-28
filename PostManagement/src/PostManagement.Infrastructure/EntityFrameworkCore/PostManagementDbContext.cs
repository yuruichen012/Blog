using System.Reflection;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

/// <summary>
/// PostManagement 数据上下文
/// </summary>
/// <param name="options">配置选项</param>
public class PostManagementDbContext(DbContextOptions<PostManagementDbContext> options, IMediator? mediator) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostManagementInfrastructureModule).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // ignore events if no dispatcher provided
        if (mediator == null)
        {
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            Clear(entity);

            foreach (var @event in events)
            {
                await mediator.Publish(@event, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    private static Action<object> _clearDomainEventsMethod = null!;
    private static void Clear(HasDomainEventsBase entity)
    {
        if (_clearDomainEventsMethod == null)
        {
            var methodInfo = typeof(HasDomainEventsBase).GetMethod("ClearDomainEvents", BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new NotSupportedException();
            }

            _clearDomainEventsMethod = instance => methodInfo.Invoke(instance, []);
        }

        _clearDomainEventsMethod(entity);
    }
}
