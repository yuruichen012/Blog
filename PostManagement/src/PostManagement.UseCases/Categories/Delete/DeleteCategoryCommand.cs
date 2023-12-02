using Ardalis.Result;
using Ardalis.SharedKernel;

namespace PostManagement.UseCases.Categories.Delete;

public record class DeleteCategoryCommand(Guid Id) : ICommand<Result>;
