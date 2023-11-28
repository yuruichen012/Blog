using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Create;

public record CreateCategoryCommand(uint ParentId, string Name) : ICommand<Result<uint>>;
