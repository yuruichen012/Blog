using Ardalis.Result;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates;
using PostManagement.Core.CategoryAggregates.Specifications;

namespace PostManagement.UseCases.Categories.List;

public class ListCategoryHandler(IRepository<Category> repository) : IQueryHandler<ListCategoryQuery, Result<IEnumerable<CategoryDTO>>>
{
    public async Task<Result<IEnumerable<CategoryDTO>>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
    {
        return await repository.ListAsync(
            new CategoryQuerySpec<CategoryDTO>(
                    x => new CategoryDTO(x.Id, x.ParentId, x.Name)
                    , skip: request.Skip
                    , take: request.Take
                )
            , cancellationToken);
    }
}
