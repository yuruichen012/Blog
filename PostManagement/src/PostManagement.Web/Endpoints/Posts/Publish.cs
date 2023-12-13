﻿using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Posts;
using PostManagement.UseCases.Posts.Pub;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Posts;

public class Publish(IMediator mediator) : Endpoint<PublishPostRequest, Result<PostDTO>>
{
    public override void Configure()
    {
        Post(PublishPostRequest.Route);
        AllowAnonymous();
        DontThrowIfValidationFails();
        Description(x => x.Accepts<PublishPostRequest>());
        Summary(x =>
        {
            x.ExampleRequest = new PublishPostRequest(0);
            x.Description = "发布文章";
        });
    }

    public override async Task HandleAsync(PublishPostRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new PublishPostCommand(req.Id), ct);
    }
}
