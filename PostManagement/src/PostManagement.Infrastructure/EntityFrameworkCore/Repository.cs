using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Events;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

public class Repository<TKey, T>(IMediator mediator, PostManagementDbContext dbContext) : IRepository<TKey, T> where T : Entity<TKey>, IAggregateRoot
{
    protected virtual DbSet<T> Set { get; } = dbContext.Set<T>();

    public virtual async ValueTask AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
    }

    public virtual async ValueTask AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await Set.AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.AnyAsync(expression, cancellationToken);
    }

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return Set.CountAsync(cancellationToken);
    }

    public virtual Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.CountAsync(predicate, cancellationToken);
    }

    public virtual Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        return Set.LongCountAsync(cancellationToken);
    }

    public virtual Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.LongCountAsync(predicate, cancellationToken);
    }

    public virtual Task<T> FirstAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.FirstAsync(expression, cancellationToken);
    }

    public virtual Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual ValueTask<T?> GetAsync(TKey? id, CancellationToken cancellationToken = default)
    {
        return Set.FindAsync([id], cancellationToken);
    }

    public virtual Task<List<T>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return Set.ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetListAsync<TInstanceType>(Expression<Func<T, TInstanceType>> selector, CancellationToken cancellationToken = default)
    {
        return Set.Select(selector).ToListAsync(cancellationToken);
    }

    public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetListAsync<TInstanceType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TInstanceType>> selector, CancellationToken cancellation = default)
    {
        return Set.Where(predicate).Select(selector).ToListAsync(cancellation);
    }

    public virtual Task<List<T>> GetPagedResultAsync(int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetPagedResultAsync<TInstanceType>(Expression<Func<T, TInstanceType>> selector, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Select(selector).Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public virtual Task<List<T>> GetPagedResultAsync(Expression<Func<T, bool>> predicate, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetPagedResultAsync<TInstanceType>(Expression<Func<T, bool>> predicate, Expression<Func<T, TInstanceType>> selector, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Skip(skip).Take(totalCount).Select(selector).ToListAsync(cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = dbContext.ChangeTracker.Entries<IAggregateRoot>().ToList();

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

        var result = await dbContext.SaveChangesAsync(cancellationToken);

        // 聚合根保存发布
        foreach (var entry in entries)
        {
            await mediator.Publish(AggregateRootSavedEvent.GetNotificationFrom(entry.Entity), cancellationToken);
        }

        return result;
    }

    public virtual Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.SingleAsync(expression, cancellationToken);
    }

    public virtual Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.SingleOrDefaultAsync(expression, cancellationToken);
    }

    public virtual ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        return ValueTask.CompletedTask;
    }

    public virtual ValueTask UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Set.UpdateRange(entities);
        return ValueTask.CompletedTask;
    }
}
