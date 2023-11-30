using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Update;

public record class UpdateCategoryCommand(uint Id, uint ParentId, string Name) : ICommand<Result>;
