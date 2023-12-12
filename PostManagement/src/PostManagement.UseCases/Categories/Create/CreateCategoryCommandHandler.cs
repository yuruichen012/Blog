using Ardalis.Result;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel;

namespace PostManagement.UseCases.Categories.Create;

/// <summary>
/// 创建类别
/// </summary>
public class CreateCategoryCommandHandler(IRepository<int, Category> repository) : IRequestHandler<CreateCategoryCommand, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.ParentId, request.Name);

        await repository.AddAsync(category, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success<CategoryDTO>(category);
    }
}
