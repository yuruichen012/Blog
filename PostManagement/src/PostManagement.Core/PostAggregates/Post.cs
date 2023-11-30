using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using PostManagement.Core.PostAggregates.Events;
using Shared.Ddd;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章
/// </summary>
public class Post : EntityBase<uint>, IAggregateRoot
{
    /// <inheritdoc/>
    public ConcurrencyStamp ConcurrencyStamp { get; private set; } = null!;

    /// <inheritdoc/>
    public DeletionStatus DeletionStatus { get; private set; } = null!;

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; private set; } = null!;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; private set; } = null!;

    /// <summary>
    /// 分类
    /// </summary>
    public PostCategory Category { get; private set; } = null!;

    /// <summary>
    /// 作者
    /// </summary>
    public PostAuthor Author { get; private set; } = null!;

    /// <summary>
    /// 状态
    /// </summary>
    public PostStatus Status { get; private set; } = null!;

    protected Post() { }

    public Post(string title, string content, PostAuthor author)
    {
        Title = Guard.Against.Null(title);
        Content = Guard.Against.Null(content);
        Author = author;

        RegisterDomainEvent(new PostDraftedEvent(title, content, author));
    }
}
