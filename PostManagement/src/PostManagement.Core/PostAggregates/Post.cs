using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using PostManagement.Core.PostAggregates.Events;
using PostManagement.Core.PostAggregates.Exceptions;
using Shared.Ddd;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章
/// </summary>
public class Post : EntityBase<Guid>, IAggregateRoot
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

    public Post(string title, string content, PostAuthor author, PostCategory category)
    {
        Title = Guard.Against.Null(title);
        Content = Guard.Against.Null(content);
        Author = Guard.Against.Null(author);
        Category = Guard.Against.Null(category);
        Status = PostStatus.Draft;

        ConcurrencyStamp = ConcurrencyStamp.Create();
        DeletionStatus = DeletionStatus.Valid;

        RegisterDomainEvent(new PostCreatedToDraftEvent(title, content, author, category));
    }

    private void CheckSaved()
    {
        if (Id == default)
        {
            throw new PostChangedBeforeSaveException();
        }
    }

    public void SetTitle(string title)
    {
        CheckSaved();

        Title = Guard.Against.Null(title);

        RegisterDomainEvent(new PostTitleChangedEvent());
    }
}
