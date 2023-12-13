using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Posts.Data;
using PostManagement.UseCases.Posts.Update;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Posts;

public class Update(IMediator mediator) : Endpoint<UpdatePostRequest, Result<PostDTO>>
{
    public override void Configure()
    {
        Put(UpdatePostRequest.Route);
        AllowAnonymous();
        DontThrowIfValidationFails();
        Summary(x =>
        {
            x.ExampleRequest = new UpdatePostRequest(0, "Title", "Content");
            x.Description = "更新文章";
        });
    }

    public override async Task HandleAsync(UpdatePostRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new UpdatePostCommand(req.Id, req.Title, req.Content), ct);
    }
}
