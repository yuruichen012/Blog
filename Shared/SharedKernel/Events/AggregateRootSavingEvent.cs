using System.Collections.Frozen;
using System.Linq.Expressions;
using MediatR;

namespace SharedKernel.Events;

/// <summary>
/// 聚合根保存事件
/// </summary>
public record class AggregateRootSavingEvent<TAggregateRoot>(TAggregateRoot AggregateRoot) : INotification where TAggregateRoot : IAggregateRoot;

public class AggregateRootSavingEvent
{
    private static FrozenDictionary<Type, Func<IAggregateRoot, INotification>> _cache = null!;

    public static INotification GetNotificationFrom(IAggregateRoot aggregateRoot)
    {
        var realType = aggregateRoot.GetType();

        if (_cache == null || _cache.TryGetValue(realType, out var newer) == false)
        {
            var map = _cache == null ? [] : new Dictionary<Type, Func<IAggregateRoot, INotification>>(_cache);

            // 将 IAggregateRoot 转为实际类型
            var rootParameter = Expression.Parameter(typeof(IAggregateRoot), "root");
            var convert = Expression.Convert(rootParameter, realType);

            // 获取构造方法
            var constructor = typeof(AggregateRootSavingEvent<>).MakeGenericType(realType).GetConstructors()[0];

            // 准备编译成委托
            var lambda = Expression.Lambda<Func<IAggregateRoot, INotification>>(
                Expression.New(constructor, convert),
                rootParameter);

            // 委托缓存
            map[realType] = newer = lambda.Compile();

            _cache = map.ToFrozenDictionary();
        }

        return newer(aggregateRoot);
    }
}
