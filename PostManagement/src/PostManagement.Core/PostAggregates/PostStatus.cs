using Ardalis.SmartEnum;

namespace PostManagement.Core.PostAggregates;

/// <summary>
/// 文章状态
/// </summary>
public sealed class PostStatus(string name, int value) : SmartEnum<PostStatus>(name, value)
{
    public static readonly PostStatus Draft = new(nameof(Draft), 1);
    public static readonly PostStatus Published = new(nameof(Published), 2);
}