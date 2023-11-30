using Ardalis.SharedKernel;

namespace PostManagement.Core.PostAggregates.Events;

public class PostDraftedEvent(string title, string content, PostAuthor author) : DomainEventBase
{
    public string Title { get; init; } = title;

    public string Content { get; init; } = content;

    public PostAuthor Author { get; init; } = author;
}
