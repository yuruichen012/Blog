using Ardalis.SharedKernel;

namespace PostManagement.Core.CategoryAggregates.Events;

/// <summary>
/// 类别删除事件
/// </summary>
public class CategoryDeletedEvent(Guid id) : DomainEventBase
{
    /// <summary>
    /// 标记
    /// </summary>
    public Guid Id { get; init; } = id;
}
