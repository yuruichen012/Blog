using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace PostManagement.Infrastructure.EntityFrameworkCore;

public class Repository<TKey, T>(IMediator mediator, PostManagementDbContext dbContext) : IRepository<TKey, T> where T : Entity<TKey>, IAggregateRoot
{
    protected virtual DbSet<T> Set { get; } = dbContext.Set<T>();

    public async ValueTask AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
    }

    public async ValueTask AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await Set.AddRangeAsync(entities, cancellationToken);
    }

    public ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        return ValueTask.CompletedTask;
    }

    public ValueTask UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Set.UpdateRange(entities);
        return ValueTask.CompletedTask;
    }

    public Task<List<T>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return Set.ToListAsync(cancellationToken);
    }

    public Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<List<T>> GetPagedResultAsync(int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public Task<List<T>> GetPagedResultAsync(Expression<Func<T, bool>> predicate, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public Task<T> FirstAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.FirstAsync(expression, cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public ValueTask<T?> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return Set.FindAsync([ id ], cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries<IAggregateRoot>())
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

        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.SingleAsync(expression, cancellationToken);
    }

    public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.SingleOrDefaultAsync(expression, cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return Set.AnyAsync(expression, cancellationToken);
    }
}
