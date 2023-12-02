using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Create;

public record CreateCategoryCommand(Guid ParentId, string Name) : ICommand<Result<Guid>>;
