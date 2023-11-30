using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.UseCases.Categories;
using PostManagement.UseCases.Categories.Get;

namespace PostManagement.Web.Categories;

public class GetById(IMediator mediator) : Endpoint<GetByIdCategoryRequest, Result<CategoryDTO>>
{
    public override void Configure()
    {
        Get(GetByIdCategoryRequest.Route);
        Options(x => x.WithTags(nameof(Category)));
        AllowAnonymous();
        Summary(x =>
        {
            x.Params[nameof(GetByIdCategoryRequest.Id)] = "1";
        });
    }

    public override async Task HandleAsync(GetByIdCategoryRequest req, CancellationToken ct)
    {
        Response = await mediator.Send(new GetCategoryQuery(req.Id), ct);
    }
}
