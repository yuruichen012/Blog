using Ardalis.SharedKernel;
using MediatR;
using PostManagement.Core.CategoryAggregates.Events;
using PostManagement.Core.CategoryAggregates.Exceptions;
using PostManagement.Core.CategoryAggregates.Specifications;

namespace PostManagement.Core.CategoryAggregates.Handlers;

public class CategoryCreatedHandler(IRepository<Category> repository) : INotificationHandler<CategoryCreatedEvent>
{
    public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ParentId == 0 || await repository.AnyAsync(new AnyByIdSepc(notification.ParentId), cancellationToken))
        {
            return;
        }

        throw new CategoryParentCategoryDoesNotExistException();
    }
}
