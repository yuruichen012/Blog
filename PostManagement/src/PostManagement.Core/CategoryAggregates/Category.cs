using PostManagement.Core.CategoryAggregates.Events;
using SharedKernel;

namespace PostManagement.Core.CategoryAggregates;

/// <summary>
/// 类别
/// </summary>
public class Category : Entity<int>, IAggregateRoot
{
    /// <summary>
    /// 父节点
    /// </summary>
    public int ParentId { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// for orm
    /// </summary>
    protected Category() { }

    /// <summary>
    /// 创建类别
    /// </summary>
    public Category(int parentId, string name)
    {
        ParentId = parentId;
        Name = name;

        AddDomainEvent(new CategoryCreatedDomainEvent(this));
    }

    /// <summary>
    /// 设置新父节点
    /// </summary>
    public void SetParentId(int parentId) 
    {
        ParentId = parentId;

        AddDomainEvent(new CategoryParentChangedDomainEvent(this));
    }

    /// <summary>
    /// 设置新名称
    /// </summary>
    public void SetName(string name)
    {
        Name = name;

        AddDomainEvent(new CategoryNameChangedDomainEvent(this));
    }
}
