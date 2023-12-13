using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Posts.Data;

namespace PostManagement.UseCases.Posts;

/// <summary>
/// 发布文章命令
/// </summary>
public record class PublishPostCommand(int Id) : IRequest<Result<PostDTO>>;
