﻿using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories.Create;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Categories;

public class Create(IMediator mediator) : Endpoint<CreateCategoryRequest, Result<int>>
{
    public override void Configure()
    {
        Post(CreateCategoryRequest.Route);
        AllowAnonymous();
        DontThrowIfValidationFails();
        Summary(x =>
        {
            x.ExampleRequest = new CreateCategoryRequest(0, ".NET");
            x.Description = "创建类别";
        });
    }

    public override async Task HandleAsync(CreateCategoryRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new CreateCategoryCommand(req.ParentId, req.Name), ct);
    }
}
