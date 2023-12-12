﻿using Ardalis.Result;
using MediatR;

namespace PostManagement.UseCases.Posts.Update;

/// <summary>
/// 更新文章命令
/// </summary>
public record class UpdatePostCommand(int Id, string Title, string Content) : IRequest<Result<PostDTO>>;
