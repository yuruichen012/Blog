namespace PostManagement.Core.CategoryAggregates.Exceptions;

/// <summary>
/// 类别名称在持久化之前发生变化
/// </summary>
public class CategoryNameChangedBeforeSaveException() : CategoryChangedBeforeSaveException
{
}
