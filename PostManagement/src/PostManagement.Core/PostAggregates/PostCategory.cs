using Ardalis.SharedKernel;

namespace PostManagement.Core.PostAggregates;

public class PostCategory(Guid id, string name) : ValueObject
{
    public static readonly PostCategory Uncategory = new(default, "Uncategory");

    public Guid Id { get; init; } = id;

    public string Name { get; init; } = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
