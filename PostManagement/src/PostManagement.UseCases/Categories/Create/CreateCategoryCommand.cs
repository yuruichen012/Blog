using Ardalis.Result;
using MediatR;

namespace PostManagement.UseCases.Categories.Create;

/// <summary>
/// 创建类别命令
/// </summary>
public record class CreateCategoryCommand(int ParentId, string Name) : IRequest<Result<int>>;
