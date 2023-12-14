using PostManagement.UseCases.Categories.Data;

namespace PostManagement.UseCases.Categories.Services;

/// <summary>
/// 类型结构化服务
/// </summary>
public interface ICategoryStructuredService
{
    /// <summary>
    /// 通过 Id 获取类别
    /// </summary>
    ValueTask<CategoryStructuredDTO?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 通过 Id 获取类别 (不包含子节点)
    /// </summary>
    ValueTask<CategoryStructuredDTO?> GetCategoryWithoutChildrenByIdAsync(int id, CancellationToken cancellationToken = default);
}
