using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel;

namespace PostManagement.UseCases.Categories.Update;

public class UpdateCategoryCommandHandler(IRepository<int, Category> repository) : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
{
    public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetAsync(request.Id, cancellationToken);
        if (category == null)
        {
            throw new Exception("Not Support");
        }

        if (category.ParentId != request.ParentId)
        {
            category.SetParentId(request.ParentId);
        }

        if (category.Name != request.Name)
        {
            category.SetName(request.Name);
        }

        await repository.SaveChangesAsync(cancellationToken);

        return category;
    }
}
