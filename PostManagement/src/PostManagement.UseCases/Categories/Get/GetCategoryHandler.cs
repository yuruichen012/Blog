using Ardalis.Result;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates;

namespace PostManagement.UseCases.Categories.Get;

public class GetCategoryHandler(IRepository<Category> repository) : IQueryHandler<GetCategoryQuery, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            return Result.NotFound();
        }

        return Result.Success(new CategoryDTO(category.Id, category.ParentId, category.Name));
    }
}
