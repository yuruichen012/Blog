using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories.Update;

namespace PostManagement.Web.Categories;

public class Update(IMediator mediator) : Endpoint<UpdateCategoryRequest, Result>
{
    public override void Configure()
    {
        Put(UpdateCategoryRequest.Route);
        AllowAnonymous();
        Summary(x =>
        {
            x.Params[nameof(UpdateCategoryRequest.Id)] = "1";
            x.ExampleRequest = new 
            {
                ParentId = 0,
                Name = "名称"
            };
        });
    }

    public override async Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct)
    {
        Response = await mediator.Send(new UpdateCategoryCommand(req.Id, req.ParentId, req.Name), ct);
    }
}
