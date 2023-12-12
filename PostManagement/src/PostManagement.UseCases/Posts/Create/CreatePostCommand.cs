using Ardalis.Result;
using MediatR;

namespace PostManagement.UseCases.Posts.Create;

/// <summary>
/// 创建文章命令
/// </summary>
public record class CreatePostCommand(string Title, string Content, Guid AuthorId, string AuthorName, int CategoryId) : IRequest<Result<PostDTO>>;
