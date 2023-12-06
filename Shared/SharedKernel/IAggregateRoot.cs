using MediatR;

namespace SharedKernel;

/// <summary>
/// 聚合根
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public IReadOnlyCollection<INotification>? DomainEvents { get; }

    /// <summary>
    /// 添加领域事件
    /// </summary>
    public void AddDomainEvent(INotification eventItem);

    /// <summary>
    /// 删除领域事件
    /// </summary>
    public void RemoveDomainEvent(INotification eventItem);

    /// <summary>
    /// 清理领域事件
    /// </summary>
    public void ClearDomainEvents();
}
