using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using PostManagement.Core.CategoryAggregates.Events;
using PostManagement.Core.CategoryAggregates.Exceptions;
using Shared.Ddd;

namespace PostManagement.Core.CategoryAggregates;

/// <summary>
/// 类别
/// </summary>
public class Category : EntityBase<uint>, IAggregateRoot
{
    /// <inheritdoc/>
    public ConcurrencyStamp ConcurrencyStamp { get; private set; } = null!;

    /// <inheritdoc/>
    public DeletionStatus DeletionStatus { get; private set; } = null!;

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
        ConcurrencyStamp = ConcurrencyStamp.Create();
        DeletionStatus = DeletionStatus.Valid;

        RegisterDomainEvent(new CategoryCreatedEvent(parentId, name));
    }

    private void CheckSaved()
    {
        if (Id == default)
        {
            throw new CategoryNameChangedBeforeSaveException();
        }
    }

    /// <summary>
    /// 设置名称
    /// </summary>
    /// <param name="name">名称</param>
    public void SetName(string name)
    {
        CheckSaved();

        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));

        RegisterDomainEvent(new CategoryNameChangedEvent(Id, name));
    }

    /// <summary>
    /// 删除
    /// </summary>
    public void Remove()
    {
        CheckSaved();

        DeletionStatus = DeletionStatus.Invalid;

        RegisterDomainEvent(new CategoryDeletedEvent(Id));
    }
}
