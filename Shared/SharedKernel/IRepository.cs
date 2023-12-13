using System.Linq.Expressions;

namespace SharedKernel;

/// <summary>
/// 仓储
/// </summary>
public interface IRepository<TKey, T> where T : Entity<TKey>, IAggregateRoot
{
    /// <summary>
    /// 获取实体
    /// </summary>
    ValueTask<T?> GetAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取集合
    /// </summary>
    Task<List<T>> GetListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取集合
    /// </summary>
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    Task<List<T>> GetPagedResultAsync(int skip, int totalCount, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取分页数据
    /// </summary>
    Task<List<T>> GetPagedResultAsync(Expression<Func<T, bool>> predicate, int skip, int totalCount, CancellationToken cancellationToken = default);

    /// <summary>
    /// 添加
    /// </summary>
    ValueTask AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 添加
    /// </summary>
    ValueTask AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新
    /// </summary>
    ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新
    /// </summary>
    ValueTask UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取第一条
    /// </summary>
    Task<T> FirstAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取第一条或默认
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取一条
    /// </summary>
    Task<T> SingleAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取一条或默认
    /// </summary>
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// 存在
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken= default);

    /// <summary>
    /// 保存
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
