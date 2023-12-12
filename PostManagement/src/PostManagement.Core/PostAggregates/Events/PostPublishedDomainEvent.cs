using MediatR;

namespace PostManagement.Core.PostAggregates.Events;

public record class PostPublishedDomainEvent(Post post) : INotification;
