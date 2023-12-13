using Ardalis.Result;
using MediatR;
using PostManagement.Core.PostAggregates;
using PostManagement.Core.PostAggregates.Services;
using PostManagement.UseCases.Posts.Data;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.UseCases.Posts;

/// <summary>
/// 创建文章
/// </summary>
public class CreatePostCommandHandler(IRepository<int, Post> repository, IPostDomainService domainService) : IRequestHandler<CreatePostCommand, Result<PostDTO>>
{
    public async Task<Result<PostDTO>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await domainService.CreateAsync(request.Title, request.Content, request.AuthorId, request.AuthorName, request.CategoryId, cancellationToken);

            await repository.AddAsync(post, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success<PostDTO>(post);
        }
        catch (ObjectNotFoundException ex)
        {
            return Result.Error(ex.Code);
        }
    }
}
