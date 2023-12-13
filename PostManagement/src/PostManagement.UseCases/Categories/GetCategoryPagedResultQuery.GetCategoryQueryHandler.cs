using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryPagedResultQuery, Result<PagedResult<CategoryDTO>>>
{
    public async Task<Result<PagedResult<CategoryDTO>>> Handle(GetCategoryPagedResultQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
