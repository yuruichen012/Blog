using SharedKernel;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章作者
/// </summary>
public record class PostAuthor(Guid Id, string Name) : ValueObject
{
    public static PostAuthor From(Guid id, string name)
    {
        return new(id, name);
    }
}
