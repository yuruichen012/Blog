using SharedKernel;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章类别
/// </summary>
public class PostCategory(int id, string name) : ValueObject
{
    public int Id { get; init; } = id;

    public string Name { get; init; } = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public static PostCategory From(int id, string name)
    {
        return new(id, name);
    }
}
