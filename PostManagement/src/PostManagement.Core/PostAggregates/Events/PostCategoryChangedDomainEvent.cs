using MediatR;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章类别更改领域事件
/// </summary>
public record class PostCategoryChangedDomainEvent(Post post) : INotification;
