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

    public async Task<T> FirstAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Set.FirstAsync(expression, cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Set.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async ValueTask<T?> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync([ id ], cancellationToken);
    }

    public async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Set.ToListAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Set.Where(expression).ToListAsync(cancellationToken);
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

    public async Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Set.SingleAsync(expression, cancellationToken);
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Set.SingleOrDefaultAsync(expression, cancellationToken);
    }
}
