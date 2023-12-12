using MediatR;

namespace PostManagement.Core.PostAggregates.Events;

/// <summary>
/// 文章内容更改领域事件
/// </summary>
public record class PostContentChangedDomainEvent(Post Post) : INotification;
