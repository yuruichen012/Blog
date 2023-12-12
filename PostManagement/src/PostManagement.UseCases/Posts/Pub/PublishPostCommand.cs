using Ardalis.Result;
using MediatR;

namespace PostManagement.UseCases.Posts.Pub;

/// <summary>
/// 发布文章命令
/// </summary>
public record class PublishPostCommand(int Id) : IRequest<Result<PostDTO>>;
