using PostManagement.Core.PostAggregates.Events;
using SharedKernel;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章
/// </summary>
public class Post : Entity<int>, IAggregateRoot
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; private set; } = null!;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; private set; } = null!;

    /// <summary>
    /// 作者
    /// </summary>
    public PostAuthor Author { get; private set; } = null!;

    /// <summary>
    /// 状态
    /// </summary>
    public PostStatus Status { get; private set; } = null!;

    /// <summary>
    /// for orm
    /// </summary>
    protected Post() { }

    /// <summary>
    /// 创建文章
    /// </summary>
    public Post(string title, string content, PostAuthor author)
    {
        Title = title;
        Content = content;
        Author = author;
        Status = PostStatus.Draft;

        AddDomainEvent(new PostCreatedToDraftDomainEvent(this));
    }
}
