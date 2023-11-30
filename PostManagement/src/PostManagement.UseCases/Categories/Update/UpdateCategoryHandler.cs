using Ardalis.Result;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates;

namespace PostManagement.UseCases.Categories.Update;

public class UpdateCategoryHandler(IRepository<Category> repository) : ICommandHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            return Result.NotFound();
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

        return Result.Success();
    }
}
