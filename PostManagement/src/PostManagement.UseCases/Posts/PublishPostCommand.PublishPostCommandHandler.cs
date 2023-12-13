using Ardalis.Result;
using MediatR;
using PostManagement.Core.PostAggregates;
using PostManagement.UseCases.Posts.Data;
using SharedKernel;

namespace PostManagement.UseCases.Posts;

/// <summary>
/// 发布文章
/// </summary>
public class PublishPostCommandHandler(IRepository<int, Post> repository) : IRequestHandler<PublishPostCommand, Result<PostDTO>>
{
    public async Task<Result<PostDTO>> Handle(PublishPostCommand request, CancellationToken cancellationToken)
    {
        var post = await repository.GetAsync(request.Id, cancellationToken);
        if (post == null)
        {
            return Result.NotFound();
        }

        post.Publish();

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success<PostDTO>(post);
    }
}
