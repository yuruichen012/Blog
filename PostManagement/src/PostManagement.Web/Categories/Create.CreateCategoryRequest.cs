namespace PostManagement.Web.Categories;

public class CreateCategoryRequest
{
    public const string Route = "/Category";

    /// <summary>
    /// 父节点
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = default!;
}
