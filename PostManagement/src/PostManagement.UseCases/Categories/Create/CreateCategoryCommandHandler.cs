using Ardalis.Result;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.UseCases.Categories.Create;

/// <summary>
/// 创建类别
/// </summary>
public class CreateCategoryCommandHandler(IRepository<int, Category> repository) : IRequestHandler<CreateCategoryCommand, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Category(request.ParentId, request.Name);

            await repository.AddAsync(category, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success<CategoryDTO>(category);
        }
        catch (ObjectNotFoundException ex)
        {
            return Result.Error(ex.Code);
        }
    }
}
