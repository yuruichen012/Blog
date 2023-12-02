using Ardalis.SharedKernel;

namespace PostManagement.Core.PostAggregates.Events;

public class PostCreatedToDraftEvent(string title, string content, PostAuthor author, PostCategory category) : DomainEventBase
{
    public string Title { get; init; } = title;

    public string Content { get; init; } = content;

    public PostAuthor Author { get; init; } = author;

    public PostCategory Category { get; init; } = category;
}
