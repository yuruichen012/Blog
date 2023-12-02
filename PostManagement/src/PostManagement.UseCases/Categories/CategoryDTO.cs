namespace PostManagement.UseCases.Categories;

public record class CategoryDTO(Guid Id, Guid ParentId, string Name);
