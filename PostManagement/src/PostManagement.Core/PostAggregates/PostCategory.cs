using SharedKernel;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章类别
/// </summary>
public record class PostCategory(int Id, string Name) : ValueObject
{
    public static PostCategory From(int id, string name)
    {
        return new(id, name);
    }
}
