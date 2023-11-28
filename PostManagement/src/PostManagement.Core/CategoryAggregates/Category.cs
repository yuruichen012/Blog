using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates.Events;
using PostManagement.Core.CategoryAggregates.Exceptions;

namespace PostManagement.Core.CategoryAggregates;

/// <summary>
/// 类别
/// </summary>
public class Category : EntityBase<uint>, IAggregateRoot
{
    /// <summary>
    /// 父节点
    /// </summary>
    public uint ParentId { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; } = default!;

    protected Category() { }

    /// <summary>
    /// 创建类别
    /// </summary>
    /// <param name="parentId">父节点</param>
    /// <param name="name">名称</param>
    public Category(uint parentId, string name)
    {
        ParentId = parentId;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

        RegisterDomainEvent(new CategoryCreatedEvent(parentId, name));
    }

    /// <summary>
    /// 设置名称
    /// </summary>
    /// <param name="name">名称</param>
    public void SetName(string name)
    {
        if (Id == default)
        {
            throw new CategoryNameChangedBeforePersistenceException();
        }

        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

        RegisterDomainEvent(new CategoryNameChangedEvent(Id, name));
    }
}
