using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories;
using PostManagement.UseCases.Categories.Create;
using PostManagement.UseCases.Categories.Data;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Categories;

public class Create(IMediator mediator) : Endpoint<CreateCategoryRequest, Result<CategoryDTO>>
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
