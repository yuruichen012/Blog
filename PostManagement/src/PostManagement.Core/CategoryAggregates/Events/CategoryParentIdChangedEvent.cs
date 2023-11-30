using Ardalis.SharedKernel;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类型父节点更改事件
/// </summary>
public class CategoryParentIdChangedEvent(uint id, uint parentId) : DomainEventBase
{
    public uint Id { get; init; } = id;
    public uint ParentId { get; init; } = parentId;
}
