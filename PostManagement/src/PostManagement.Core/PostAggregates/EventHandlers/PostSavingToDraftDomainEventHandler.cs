using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Exceptions;

namespace PostManagement.Core.PostAggregates.EventHandlers;

public class PostSavingToDraftDomainEventHandler(IRepository<int, Category> repository) : INotificationHandler<AggregateRootSavingEvent<Post>>
{
    public async Task Handle(AggregateRootSavingEvent<Post> notification, CancellationToken cancellationToken)
    {
        if (await repository.AnyAsync(x => x.Id == notification.AggregateRoot.Category.Id, cancellationToken) == false)
        {
            throw new ObjectNotFoundException("Post.Category.NotFound");
        }
    }
}
