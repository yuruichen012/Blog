using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.List;

public record class ListCategoryQuery(int Skip = 0, int Take = 10) : IQuery<Result<IEnumerable<CategoryDTO>>>;
