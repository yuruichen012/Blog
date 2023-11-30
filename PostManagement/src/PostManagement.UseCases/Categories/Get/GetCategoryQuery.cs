using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Get;

public record class GetCategoryQuery(uint Id) : IQuery<Result<CategoryDTO>>;
