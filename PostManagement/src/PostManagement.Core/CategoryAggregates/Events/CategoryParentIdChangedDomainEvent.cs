using MediatR;

namespace PostManagement.Core.CategoryAggregates;

/// <summary>
/// 类别父节点更改领域事件
/// </summary>
public record class CategoryParentIdChangedDomainEvent(Category Category) : INotification;
