using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.Core.CategoryAggregates;
using PostManagement.UseCases.Categories.Create;

namespace PostManagement.Web.Categories;

public class Create(IMediator mediator) : Endpoint<CreateCategoryRequest, Result<uint>>
{
    public override void Configure()
    {
        Post(CreateCategoryRequest.Route);
        Options(x => x.WithTags(nameof(Category)));
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CreateCategoryRequest()
            {
                ParentId = 0,
                Name = "测试"
            };
        });
    }

    public override async Task HandleAsync(CreateCategoryRequest req, CancellationToken ct)
    {
        Response = await mediator.Send(new CreateCategoryCommand(req.ParentId, req.Name), ct);
    }
}
