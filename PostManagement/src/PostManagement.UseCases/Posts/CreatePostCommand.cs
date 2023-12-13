using Ardalis.Result;
using MediatR;
using PostManagement.UseCases.Posts.Data;

namespace PostManagement.UseCases.Posts;

/// <summary>
/// 创建文章命令
/// </summary>
public record class CreatePostCommand(string Title, string Content, Guid AuthorId, string AuthorName, int CategoryId) : IRequest<Result<PostDTO>>;
