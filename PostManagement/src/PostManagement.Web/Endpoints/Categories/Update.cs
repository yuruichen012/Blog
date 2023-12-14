using Ardalis.Result;
using FastEndpoints;
using MediatR;
using PostManagement.UseCases.Categories.Data;
using PostManagement.UseCases.Categories.Update;
using PostManagement.Web.Extensions;

namespace PostManagement.Web.Endpoints.Categories;

public class Update(IMediator mediator) : Endpoint<UpdateCategoryRequest, Result<CategoryDTO>>
{
    public override void Configure()
    {
        Put(UpdateCategoryRequest.Route);
        Options(x => x.WithTags("Category"));
        AllowAnonymous();
        DontThrowIfValidationFails();
        Summary(x =>
        {
            x.ExampleRequest = new
            {
                ParentId = 0,
                Name = "新名称"
            };

            x.Description = "更新类别";
        });
    }

    public override async Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct)
    {
        if (this.ReturnValidationErrorsIfInvalid())
        {
            return;
        }

        Response = await mediator.Send(new UpdateCategoryCommand(req.Id, req.ParentId, req.Name), ct);
    }
}
