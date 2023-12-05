using MediatR;

namespace PostManagement.Core.PostAggregates.Events;

public record class PostCreatedToDraftDomainEvent(Post Post) : INotification;
