using Ardalis.SharedKernel;

namespace PostManagement.Core.PostAggregates;

public class PostCategory(int id, string name) : ValueObject
{
    public static readonly PostCategory Uncategory = new(0, "Uncategory");

    public int Id { get; init; } = id;

    public string Name { get; init; } = name;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
