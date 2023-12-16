using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories.Services;

/// <summary>
/// 类型树形结构服务
/// </summary>
public interface ICategoryTreeService
{
    /// <summary>
    /// 通过 Id 获取类别
    /// </summary>
    ValueTask<CategoryTreeNodeDTO?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 通过 Id 获取类别 (不包含子节点)
    /// </summary>
    ValueTask<CategoryTreeNodeDTO?> GetCategoryWithoutChildrenByIdAsync(int id, CancellationToken cancellationToken = default);
}
