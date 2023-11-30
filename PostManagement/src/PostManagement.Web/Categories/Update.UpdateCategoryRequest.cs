namespace PostManagement.Web.Categories;

public class UpdateCategoryRequest
{
    public const string Route = "/Category/{Id}";

    public uint Id { get; set; }

    /// <summary>
    /// 父节点
    /// </summary>
    public uint ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = null!;
}
