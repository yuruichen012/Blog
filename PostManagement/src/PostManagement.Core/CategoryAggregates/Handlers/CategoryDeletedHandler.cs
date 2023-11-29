using Ardalis.SharedKernel;
using MediatR;
using PostManagement.Core.CategoryAggregates.Events;
using PostManagement.Core.CategoryAggregates.Exceptions;
using PostManagement.Core.CategoryAggregates.Specifications;

namespace PostManagement.Core.CategoryAggregates.Handlers;

public class CategoryDeletedHandler(IRepository<Category> repository) : INotificationHandler<CategoryDeletedEvent>
{
    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        if (await repository.AnyAsync(new AnyByParentIdSpec(notification.Id), cancellationToken))
        {
            throw new SubcategoriesUnderTheCategoryException();
        }
    }
}
