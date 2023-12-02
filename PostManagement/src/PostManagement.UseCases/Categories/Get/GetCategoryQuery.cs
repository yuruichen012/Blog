using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Get;

public record class GetCategoryQuery(Guid Id) : IQuery<Result<CategoryDTO>>;
