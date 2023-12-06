using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PostManagement.UseCases.Categories.Create;

namespace PostManagement.Web.Categories;

public class Create(IMediator mediator) : Endpoint<CreateCategoryRequest, Results<Ok<int>, NotFound, BadRequest<string>>>
{
    public override void Configure()
    {
        Post(CreateCategoryRequest.Route);
        AllowAnonymous();
        Summary(x =>
        {
            x.ExampleRequest = new CreateCategoryRequest(0, ".NET");
            x.Description = "创建类别";
        });
    }

    public override async Task<Results<Ok<int>, NotFound, BadRequest<string>>> ExecuteAsync(CreateCategoryRequest req, CancellationToken ct)
    {
        try
        {
            return TypedResults.Ok(await mediator.Send(new CreateCategoryCommand(req.ParentId, req.Name), ct));
        }
        catch
        {
            return TypedResults.BadRequest("");
        }
    }
}
