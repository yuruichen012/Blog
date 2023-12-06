using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories.Create;

namespace PostManagement.Web.Categories;

public class Create(IMediator mediator) : Endpoint<CreateCategoryRequest, int>
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

    public override async Task HandleAsync(CreateCategoryRequest req, CancellationToken ct)
    {
        Response = await mediator.Send(new CreateCategoryCommand(req.ParentId, req.Name), ct);
    }
}
