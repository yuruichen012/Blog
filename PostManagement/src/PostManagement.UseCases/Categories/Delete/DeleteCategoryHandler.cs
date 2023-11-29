using Ardalis.Result;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates;

namespace PostManagement.UseCases.Categories.Delete;

public class DeleteCategoryHandler(IRepository<Category> repository) : ICommandHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            return Result.NotFound();
        }

        category.Remove();
        await repository.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
