using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel;

namespace PostManagement.UseCases.Categories.Create;

public class CreateCategoryCommandHandler(IRepository<int, Category> repository) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.ParentId, request.Name);

        await repository.AddAsync(category, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
