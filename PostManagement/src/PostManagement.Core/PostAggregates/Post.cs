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
    /// 类别
    /// </summary>
    public PostCategory Category { get; set; } = null!;

    /// <summary>
    /// 状态
    /// </summary>
    public PostStatus Status { get; private set; } = null!;

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishedTime { get; private set; }

    /// <summary>
    /// for orm
    /// </summary>
    protected Post() { }

    /// <summary>
    /// 创建文章
    /// </summary>
    public Post(string title, string content, PostAuthor author, PostCategory category)
    {
        Title = title;
        Content = content;
        Author = author;
        Category = category;
        Status = PostStatus.Draft;

        AddDomainEvent(new PostCreatedToDraftDomainEvent(this));
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    public void SetTitle(string title)
    {
        Title = title;

        AddDomainEvent(new PostTitleChangedDomainEvent(this));
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    public void SetContent(string content)
    {
        Content = content;

        AddDomainEvent(new PostContentChangedDomainEvent(this));
    }

    /// <summary>
    /// 设置类别
    /// </summary>
    public void SetCategory(PostCategory category)
    {
        Category = category;

        AddDomainEvent(new PostCategoryChangedDomainEvent(this));
    }

    /// <summary>
    /// 发布文章
    /// </summary>
    public void Publish()
    {
        if (Status != PostStatus.Draft)
        {
            return;
        }

        Status = PostStatus.Published;
        PublishedTime = DateTime.Now;

        AddDomainEvent(new PostPublishedDomainEvent(this));
    }
}
