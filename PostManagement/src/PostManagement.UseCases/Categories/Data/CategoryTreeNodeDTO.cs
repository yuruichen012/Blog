namespace PostManagement.UseCases.Categories.Data;

/// <summary>
/// 类别结构化传输对象
/// </summary>
public record class CategoryTreeNodeDTO(int Id, int ParentId, string Name, CategoryTreeNodeDTO[] Children);
