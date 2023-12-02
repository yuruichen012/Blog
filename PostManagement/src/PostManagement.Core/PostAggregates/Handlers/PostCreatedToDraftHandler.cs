using Ardalis.SharedKernel;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.Core.CategoryAggregates.Specifications;
using PostManagement.Core.PostAggregates.Events;
using PostManagement.Core.PostAggregates.Exceptions;

namespace PostManagement.Core.PostAggregates.Handlers;

public class PostCreatedToDraftHandler(IRepository<Category> repository) : INotificationHandler<PostCreatedToDraftEvent>
{
    public async Task Handle(PostCreatedToDraftEvent notification, CancellationToken cancellationToken)
    {
        if (await repository.AnyAsync(new CategoryByIdSepc(notification.Category.Id), cancellationToken))
        {
            return;
        }

        throw new PostCategoryDoesNotExistException();
    }
}
