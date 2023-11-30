using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.UseCases.Categories;
using PostManagement.UseCases.Categories.List;

namespace PostManagement.Web.Categories;

public class List(IMediator mediator) : Endpoint<ListCategoryRequest, Result<IEnumerable<CategoryDTO>>>
{
    public override void Configure()
    {
        Get(ListCategoryRequest.Route);
        Options(x => x.WithTags(nameof(Category)));
        AllowAnonymous();
        Summary(x => 
        {
            x.Params["skip"] = "0";
            x.Params["take"] = "10";
        });
    }

    public override async Task HandleAsync(ListCategoryRequest req, CancellationToken ct)
    {
        Response = await mediator.Send(new ListCategoryQuery(req.Skip, req.Take), ct);
    }
}
