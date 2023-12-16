using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;
using PostManagement.UseCases.Categories.Services;

namespace PostManagement.UseCases.Categories;

public class GetCategoryTreeQueryHandler(ICategoryTreeService categoryTreeService) : IRequestHandler<GetCategoryTreeQuery, Result<CategoryTreeNodeDTO>>
{
    public async Task<Result<CategoryTreeNodeDTO>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
    {
        var result = await categoryTreeService.GetCategoryByIdAsync(request.Id, cancellationToken);
        if (result == null)
        {
            return Result.NotFound();
        }

        return Result.Success(result);
    }
}
