using Ardalis.SharedKernel;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 作者
/// </summary>
public class PostAuthor(int id, string name) : ValueObject
{
    public int Id { get; init; } = id;
    public string Name { get; init; } = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
