using MediatR;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 创建类别领域事件
/// </summary>
public record class CategoryCreatedDomainEvent(Category Category) : INotification;
