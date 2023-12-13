using Ardalis.Result;
using MediatR;
using PostManagement.Core.PostAggregates;
using PostManagement.UseCases.Posts.Data;
using PostManagement.UseCases.Posts.Update;
using SharedKernel;

namespace PostManagement.UseCases.Posts;

/// <summary>
/// 更新文章
/// </summary>
public class UpdatePostCommandHandler(IRepository<int, Post> repository) : IRequestHandler<UpdatePostCommand, Result<PostDTO>>
{
    public async Task<Result<PostDTO>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await repository.GetAsync(request.Id, cancellationToken);
        if (post == null)
        {
            return Result.NotFound();
        }

        if (post.Title != request.Title)
        {
            post.SetTitle(request.Title);
        }

        if (post.Content != request.Content)
        {
            post.SetContent(request.Content);
        }

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success<PostDTO>(post);
    }
}
