using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories;

public record class GetCategoryPagedResultQuery(int PageNumber = 0, int PageSize = 10) : IRequest<PagedResult<List<CategoryDTO>>>;
