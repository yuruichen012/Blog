using PostManagement.Core.CategoryAggregates;

namespace PostManagement.UseCases.Categories;

/// <summary>
/// 类别传输对象
/// </summary>
public record class CategoryDTO(int Id, int ParentId, string Name)
{
    public static implicit operator CategoryDTO(Category category)
    {
        return new CategoryDTO(category.Id, category.ParentId, category.Name);
    }
}
