using Ardalis.SharedKernel;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类型父节点更改事件
/// </summary>
public class CategoryParentIdChangedEvent(Guid id, Guid parentId) : DomainEventBase
{
    public Guid Id { get; init; } = id;
    public Guid ParentId { get; init; } = parentId;
}
