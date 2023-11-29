using Ardalis.SharedKernel;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类别删除事件
/// </summary>
public class CategoryDeletedEvent(uint id) : DomainEventBase
{
    /// <summary>
    /// 标记
    /// </summary>
    public uint Id { get; init; } = id;
}
