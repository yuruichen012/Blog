using MediatR;

namespace PostManagement.Core.PostAggregates.Events;

/// <summary>
/// 文章发布领域事件
/// </summary>
public record class PostPublishedDomainEvent(Post Post) : INotification;
