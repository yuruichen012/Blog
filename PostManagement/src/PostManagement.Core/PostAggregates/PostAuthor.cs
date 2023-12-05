using SharedKernel;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章作者
/// </summary>
public class PostAuthor(Guid id, string name) : ValueObject
{
    public Guid Id { get; init; } = id;

    public string Name { get; init; } = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public static PostAuthor From(Guid id, string name)
    {
        return new(id, name);
    }
}