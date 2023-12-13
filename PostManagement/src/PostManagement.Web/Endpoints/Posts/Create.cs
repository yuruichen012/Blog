using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Posts;
using PostManagement.UseCases.Posts.Create;
using PostManagement.UseCases.Posts.Data;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Posts;

public class Create(IMediator mediator) : Endpoint<CreatePostRequest, Result<PostDTO>>
{
    public override void Configure()
    {
        Post(CreatePostRequest.Route);
        AllowAnonymous();
        DontThrowIfValidationFails();
        Summary(x =>
        {
            x.ExampleRequest = new CreatePostRequest("Title", "Content", 0);
            x.Description = "创建文章";
        });
    }

    public override async Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new CreatePostCommand(req.Title, req.Content, default, "Test", req.CategoryId), ct);
    }
}
