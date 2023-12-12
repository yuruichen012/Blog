using Ardalis.Result;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.UseCases.Categories.Update;

public class UpdateCategoryCommandHandler(IRepository<int, Category> repository) : IRequestHandler<UpdateCategoryCommand, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetAsync(request.Id, cancellationToken) ?? throw new ObjectNotFoundException("Category.NotFound");
        if (category.ParentId != request.ParentId)
        {
            category.SetParentId(request.ParentId);
        }

        if (category.Name != request.Name)
        {
            category.SetName(request.Name);
        }

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success<CategoryDTO>(category);
    }
}
