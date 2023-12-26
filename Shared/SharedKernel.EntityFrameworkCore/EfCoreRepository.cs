using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Events;
using System.Linq.Expressions;

namespace SharedKernel.Infrastructure;

public class EfCoreRepository<TKey, TEntity, TDbContext>(TDbContext dbContext) : IRepository<TKey, TEntity>
    where TEntity : Entity<TKey>, IAggregateRoot
    where TDbContext : DbContext
{
    protected virtual DbContext DbContext { get; } = dbContext;

    protected virtual DbSet<TEntity> Set { get; } = dbContext.Set<TEntity>();

    public virtual async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
    }

    public virtual async ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Set.AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.AnyAsync(expression, cancellationToken);
    }

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return Set.CountAsync(cancellationToken);
    }

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.CountAsync(predicate, cancellationToken);
    }

    public virtual Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        return Set.LongCountAsync(cancellationToken);
    }

    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.LongCountAsync(predicate, cancellationToken);
    }

    public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.FirstAsync(expression, cancellationToken);
    }

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual ValueTask<TEntity?> GetAsync(TKey? id, CancellationToken cancellationToken = default)
    {
        return Set.FindAsync([id], cancellationToken);
    }

    public virtual Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return Set.ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetListAsync<TInstanceType>(Expression<Func<TEntity, TInstanceType>> selector, CancellationToken cancellationToken = default)
    {
        return Set.Select(selector).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetListAsync<TInstanceType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TInstanceType>> selector, CancellationToken cancellation = default)
    {
        return Set.Where(predicate).Select(selector).ToListAsync(cancellation);
    }

    public virtual Task<List<TEntity>> GetPagedResultAsync(int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetPagedResultAsync<TInstanceType>(Expression<Func<TEntity, TInstanceType>> selector, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Select(selector).Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TEntity>> GetPagedResultAsync(Expression<Func<TEntity, bool>> predicate, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Skip(skip).Take(totalCount).ToListAsync(cancellationToken);
    }

    public virtual Task<List<TInstanceType>> GetPagedResultAsync<TInstanceType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TInstanceType>> selector, int skip, int totalCount, CancellationToken cancellationToken = default)
    {
        return Set.Where(predicate).Skip(skip).Take(totalCount).Select(selector).ToListAsync(cancellationToken);
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.SingleAsync(expression, cancellationToken);
    }

    public virtual Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return Set.SingleOrDefaultAsync(expression, cancellationToken);
    }

    public virtual ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        return ValueTask.CompletedTask;
    }

    public virtual ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Set.UpdateRange(entities);
        return ValueTask.CompletedTask;
    }
}
