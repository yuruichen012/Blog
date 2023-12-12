using MediatR;

namespace PostManagement.Core.PostAggregates.Events;

/// <summary>
/// 文章创建为草稿领域事件
/// </summary>
public record class PostCreatedToDraftDomainEvent(Post Post) : INotification;
