using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories;

/// <summary>
/// 创建类别命令
/// </summary>
public record class CreateCategoryCommand(int ParentId, string Name) : IRequest<Result<CategoryDTO>>;
