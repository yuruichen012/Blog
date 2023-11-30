using Ardalis.SmartEnum;

namespace PostManagement.Core.PostAggregates;

public class PostStatus(string name, int value) : SmartEnum<PostStatus>(name, value)
{
    public static readonly PostStatus Draft = new("Draft", 1);
    public static readonly PostStatus Published = new("Published", 2);
    public static readonly PostStatus Banned = new("Banned", 3);
}
