using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories;

public record class GetCategoryPagedResultQuery(int Skip = 0, int TotalCount = 10) : IRequest<Result<PagedResult<CategoryDTO>>>;
