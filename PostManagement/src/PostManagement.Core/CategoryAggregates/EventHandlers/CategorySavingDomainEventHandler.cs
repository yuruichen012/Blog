using MediatR;
using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Exceptions;

namespace PostManagement.Core.CategoryAggregates.EventHandlers;

public class CategorySavingDomainEventHandler(IRepository<int, Category> repository) : INotificationHandler<AggregateRootSavingEvent<Category>>
{
    public async Task Handle(AggregateRootSavingEvent<Category> notification, CancellationToken cancellationToken)
    {
        if (notification.AggregateRoot.ParentId == 0 || await repository.AnyAsync(x => x.Id == notification.AggregateRoot.ParentId, cancellationToken))
        {
            return;
        }

        throw new ObjectNotFoundException("Category.ParentId.NotFound");
    }
}
