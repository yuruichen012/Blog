using MediatR;

namespace SharedKernel;

/// <summary>
/// 实体
/// </summary>
public class Entity<TKey>
{
    private int? _requestedHashCode;
    private List<INotification>? _domainEvents;

    /// <summary>
    /// Id
    /// </summary>
    public virtual TKey Id { get; } = default!;

    /// <summary>
    /// 领域事件
    /// </summary>
    public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

    /// <summary>
    /// 添加领域事件
    /// </summary>
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? [];
        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// 删除领域事件
    /// </summary>
    public void RemoveDomainEvent(INotification eventItem)
    {
        _ = _domainEvents?.Remove(eventItem);
    }

    /// <summary>
    /// 清理领域事件
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// 新创建的
    /// </summary>
    public bool IsTransient()
    {
        return Id == null || Id.Equals(default);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null or not Entity<TKey>)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        Entity<TKey> item = (Entity<TKey>)obj;

        return !item.IsTransient() && !IsTransient() && item.Id!.Equals(Id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
            {
                _requestedHashCode = Id!.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            }

            return _requestedHashCode.Value;
        }
        else
        {
            return base.GetHashCode();
        }
    }
    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right)
    {
        return Equals(left, null) ? Equals(right, null) : left.Equals(right);
    }

    public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right)
    {
        return !(left == right);
    }
}
