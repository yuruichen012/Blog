
using PostManagement.Core.CategoryAggregates;
using SharedKernel;
using SharedKernel.Exceptions;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 领域服务 (internal)
/// </summary>
internal class PostDomainService(IRepository<int, Category> categoryRepository) : IPostDomainService
{
    public async Task<Post> CreateAsync(string title, string content, Guid authorId, string authorName, int categoryId, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(categoryId, cancellationToken);
        if (category == null)
        {
            throw new ObjectNotFoundException("Post.Category.NotFound");
        }

        return new Post(title, content, PostAuthor.From(authorId, authorName), PostCategory.From(category.Id, category.Name));
    }
}
