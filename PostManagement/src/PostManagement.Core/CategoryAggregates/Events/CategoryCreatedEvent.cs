using Ardalis.SharedKernel;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类别已创建
/// </summary>
/// <param name="parentId">父节点</param>
/// <param name="name">名称</param>
public class CategoryCreatedEvent(uint parentId, string name) : DomainEventBase
{
    /// <summary>
    /// 父节点
    /// </summary>
    public uint ParentId { get; init; } = parentId;

    /// <summary>
    /// 类别名称
    /// </summary>
    public string Name { get; init; } = name;
}
