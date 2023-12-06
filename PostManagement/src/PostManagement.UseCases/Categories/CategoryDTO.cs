using PostManagement.Core.CategoryAggregates;

namespace PostManagement.UseCases.Categories;
public record class CategoryDTO(int Id, int ParentId, string Name)
{
    public static implicit operator CategoryDTO(Category category)
    {
        return new CategoryDTO(category.Id, category.ParentId, category.Name);
    }
}
