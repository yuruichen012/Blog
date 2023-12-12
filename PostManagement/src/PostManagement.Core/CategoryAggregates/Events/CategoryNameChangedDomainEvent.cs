using MediatR;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类别名称更改领域事件
/// </summary>
public record class CategoryNameChangedDomainEvent(Category Category) : INotification;
