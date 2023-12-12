using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.Core.PostAggregates.Events;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.Core.PostAggregates.EventHandlers;

public class PostCreatedToDraftDomainEventHandler(IRepository<int, Category> repository) : INotificationHandler<PostCreatedToDraftDomainEvent>
{
    public async Task Handle(PostCreatedToDraftDomainEvent notification, CancellationToken cancellationToken)
    {
        var category = await repository.GetAsync(notification.Post.Category.Id, cancellationToken);
        if (category == null)
        {
            throw new ObjectNotFoundException("Post.Category.NotFound");
        }
    }
}
