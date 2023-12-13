using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories.Update;

/// <summary>
/// 类别更新命令
/// </summary>
public record class UpdateCategoryCommand(int Id, int ParentId, string Name) : IRequest<Result<CategoryDTO>>;
