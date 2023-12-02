namespace PostManagement.Web.Categories;

public class UpdateCategoryRequest
{
    public const string Route = "/Category/{Id}";

    public Guid Id { get; set; }

    /// <summary>
    /// 父节点
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = null!;
}
