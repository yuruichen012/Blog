using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel.Events;

namespace PostManagement.Infrastructure.Categories.Services;

public class CategorySavedEventHandler(CategoryTreeService categoryTreeService) : INotificationHandler<AggregateRootSavedEvent<Category>>
{
    public async Task Handle(AggregateRootSavedEvent<Category> notification, CancellationToken cancellationToken)
    {
        if (categoryTreeService.IsInitialized)
        {
            await categoryTreeService.InitializeAsync(cancellationToken);
        }
    }
}
