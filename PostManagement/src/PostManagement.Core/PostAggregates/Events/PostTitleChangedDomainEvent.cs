using MediatR;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章标题更改领域事件
/// </summary>
public record class PostTitleChangedDomainEvent(Post Post) : INotification;
