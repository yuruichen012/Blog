using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories;

public record class GetCategoryTreeQuery(int Id) : IRequest<Result<CategoryTreeNodeDTO>>;
