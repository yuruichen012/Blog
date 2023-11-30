﻿using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.UseCases.Categories.Delete;

namespace PostManagement.Web.Categories;

public class Delete(IMediator mediator) : Endpoint<DeleteCategoryRequest, Result>
{
    public override void Configure()
    {
        Delete(DeleteCategoryRequest.Route);
        Options(x => x.WithTags(nameof(Category)));
        AllowAnonymous();
        Summary(x =>
        {
            x.Params[nameof(DeleteCategoryRequest.Id)] = "1";
        });
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        Response = await mediator.Send(new DeleteCategoryCommand(req.Id), ct);
    }
}
