using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Update;

public record class UpdateCategoryCommand(Guid Id, Guid ParentId, string Name) : ICommand<Result>;
