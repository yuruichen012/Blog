using Ardalis.Result;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.UseCases.Categories.Data;
using SharedKernel;
using SharedKernel.Common;

namespace PostManagement.UseCases.Categories;

public class GetCategoryQueryHandler(IRepository<int, Category> repository) : IRequestHandler<GetCategoryPagedResultQuery, PagedResult<List<CategoryDTO>>>
{
    public async Task<PagedResult<List<CategoryDTO>>> Handle(GetCategoryPagedResultQuery request, CancellationToken cancellationToken)
    {
        var skip = PageHelper.GetSkip(request.PageNumber, request.PageSize);

        var recordCount = await repository.CountAsync(cancellationToken);
        var records = await repository.GetPagedResultAsync(selector: x => new CategoryDTO(x.Id, x.ParentId, x.Name), skip, request.PageSize, cancellationToken);

        var totalPages = PageHelper.GetTotalPages(request.PageSize, recordCount);

        return new PagedResult<List<CategoryDTO>>(new PagedInfo(request.PageNumber, request.PageSize, totalPages, recordCount), records);
    }
}
