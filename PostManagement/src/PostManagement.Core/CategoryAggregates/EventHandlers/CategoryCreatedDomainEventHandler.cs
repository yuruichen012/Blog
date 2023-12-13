using MediatR;
using PostManagement.Core.CategoryAggregates.Events;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.Core.CategoryAggregates.EventHandlers;

public class CategoryCreatedDomainEventHandler(IRepository<int, Category> repository) : INotificationHandler<CategoryCreatedDomainEvent>
{
    public async Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Category.ParentId == 0 || await repository.AnyAsync(x => x.Id == notification.Category.ParentId, cancellationToken))
        {
            return;
        }

        throw new ObjectNotFoundException("Category.ParentId.NotFound");
    }
}
