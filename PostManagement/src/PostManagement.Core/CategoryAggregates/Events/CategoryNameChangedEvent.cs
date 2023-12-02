using Ardalis.SharedKernel;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类别名称修改事件
/// </summary>
/// <param name="id">标识</param>
/// <param name="name">名称</param>
public class CategoryNameChangedEvent(Guid id, string name) : DomainEventBase
{
    /// <summary>
    /// 标识
    /// </summary>
    public Guid Id { get; init; } = id;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; init; } = name;
}
