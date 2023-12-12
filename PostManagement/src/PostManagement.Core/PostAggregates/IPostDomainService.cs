using SharedKernel.Exceptions;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章领域服务
/// </summary>
public interface IPostDomainService
{
    /// <summary>
    /// 创建文章
    /// </summary>
    /// <exception cref="ObjectNotFoundException">类别为空时异常</exception>
    Task<Post> CreateAsync(string title, string content, Guid authorId, string authorName, int categoryId, CancellationToken cancellationToken);
}
