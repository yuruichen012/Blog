using System.Linq.Expressions;

namespace SharedKernel;

/// <summary>
/// 仓储
/// </summary>
public interface IRepository<TKey, TEntity> where TEntity : Entity<TKey>, IAggregateRoot
{
    /// <summary>
    /// 添加
    /// </summary>
    ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 添加
    /// </summary>
    ValueTask AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 存在
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取总条数
    /// </summary>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取总条数
    /// </summary>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取总条数
    /// </summary>
    Task<long> LongCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取总条数
    /// </summary>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取第一条
    /// </summary>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取第一条或默认
    /// </summary>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取实体
    /// </summary>
    ValueTask<TEntity?> GetAsync(TKey? id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取集合
    /// </summary>
    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取集合
    /// </summary>
    Task<List<TInstanceType>> GetListAsync<TInstanceType>(Expression<Func<TEntity, TInstanceType>> selector, CancellationToken cancellation = default);

    /// <summary>
    /// 获取集合
    /// </summary>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取集合
    /// </summary>
    Task<List<TInstanceType>> GetListAsync<TInstanceType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TInstanceType>> selector, CancellationToken cancellation = default);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    Task<List<TEntity>> GetPagedResultAsync(int skip, int totalCount, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    Task<List<TInstanceType>> GetPagedResultAsync<TInstanceType>(Expression<Func<TEntity, TInstanceType>> selector, int skip, int totalCount, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    Task<List<TEntity>> GetPagedResultAsync(Expression<Func<TEntity, bool>> predicate, int skip, int totalCount, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    Task<List<TInstanceType>> GetPagedResultAsync<TInstanceType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TInstanceType>> selector, int skip, int totalCount, CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取一条
    /// </summary>
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取一条或默认
    /// </summary>
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新
    /// </summary>
    ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新
    /// </summary>
    ValueTask UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
