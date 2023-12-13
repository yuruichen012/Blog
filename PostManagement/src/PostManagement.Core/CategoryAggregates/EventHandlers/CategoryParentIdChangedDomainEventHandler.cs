using MediatR;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.Core.CategoryAggregates.EventHandlers;

public class CategoryParentIdChangedDomainEventHandler(IRepository<int, Category> repository) : INotificationHandler<CategoryParentIdChangedDomainEvent>
{
    public async Task Handle(CategoryParentIdChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        if (await repository.GetAsync(notification.Category.ParentId, cancellationToken) == null)
        {
            throw new ObjectNotFoundException("Category.ParentId.NotFound");
        }
    }
}
