using MediatR;
using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Exceptions;

namespace PostManagement.Core.CategoryAggregates.EventHandlers;

public class CategorySavedDomainEventHandler(IRepository<int, Category> repository) : INotificationHandler<AggregateRootSavedEvent<Category>>
{
    public async Task Handle(AggregateRootSavedEvent<Category> notification, CancellationToken cancellationToken)
    {
        if (notification.AggregateRoot.ParentId == 0 || await repository.AnyAsync(x => x.Id == notification.AggregateRoot.ParentId, cancellationToken))
        {
            return;
        }

        throw new ObjectNotFoundException("Category.ParentId.NotFound");
    }
}
