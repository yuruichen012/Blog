using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories;
using PostManagement.UseCases.Categories.Data;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Categories;

public class GetTree(IMediator mediator) : Endpoint<GetCategoryTreeRequest, Result<CategoryTreeNodeDTO>>
{
    public override void Configure()
    {
        Get(GetCategoryTreeRequest.Route);
        Options(x => x.WithTags("Category"));
        AllowAnonymous();
        DontThrowIfValidationFails();
        Summary(x =>
        {
            x.ExampleRequest = new GetCategoryTreeRequest(1);
            x.Description = "查询类别以及子节点";
        });
    }

    public override async Task HandleAsync(GetCategoryTreeRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new GetCategoryTreeQuery(req.Id), ct);
    }
}
