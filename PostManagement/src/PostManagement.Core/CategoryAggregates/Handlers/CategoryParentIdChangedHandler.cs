using Ardalis.SharedKernel;
using MediatR;
using PostManagement.Core.CategoryAggregates.Events;
using PostManagement.Core.CategoryAggregates.Exceptions;
using PostManagement.Core.CategoryAggregates.Specifications;

namespace PostManagement.Core.CategoryAggregates.Handlers;

public class CategoryParentIdChangedHandler(IRepository<Category> repository) : INotificationHandler<CategoryParentIdChangedEvent>
{
    public async Task Handle(CategoryParentIdChangedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ParentId == default || await repository.AnyAsync(new CategoryByIdSepc(notification.ParentId), cancellationToken))
        {
            return;
        }

        throw new CategoryParentCategoryDoesNotExistException();
    }
}
