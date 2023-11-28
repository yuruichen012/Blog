namespace PostManagement.Web.Categories;

public class CreateCategoryRequest
{
    public const string Route = "/Category";

    /// <summary>
    /// 父节点
    /// </summary>
    public uint ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;
}
