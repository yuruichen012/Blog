namespace PostManagement.UseCases.Categories.Data;

/// <summary>
/// 类别结构化传输对象
/// </summary>
public record class CategoryStructuredDTO(int Id, int ParentId, string Name, CategoryStructuredDTO[] Children);
