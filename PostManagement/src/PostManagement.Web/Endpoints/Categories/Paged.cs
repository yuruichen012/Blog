using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories;
using PostManagement.UseCases.Categories.Data;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Categories;

public class Paged(IMediator mediator) : Endpoint<PagedCategoryRequest, Result<List<CategoryDTO>>>
{
    public override void Configure()
    {
        Get(PagedCategoryRequest.Route);
        Options(x => x.WithTags("Category"));
        AllowAnonymous();
        DontThrowIfValidationFails();
        Summary(x =>
        {
            x.ExampleRequest = new CreateCategoryRequest(0, ".NET");
            x.Description = "创建类别";
        });
    }

    public override async Task HandleAsync(PagedCategoryRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new GetCategoryPagedQuery(req.PageNumber, req.PageSize), ct);
    }
}
