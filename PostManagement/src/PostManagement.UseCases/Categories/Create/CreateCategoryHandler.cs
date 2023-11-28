﻿using Ardalis.Result;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates;

namespace PostManagement.UseCases.Categories.Create;

public class CreateCategoryHandler(IRepository<Category> repository) : ICommandHandler<CreateCategoryCommand, Result<uint>>
{
    public async Task<Result<uint>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.AddAsync(new Category(request.ParentId, request.Name), cancellationToken);
        return result.Id;
    }
}