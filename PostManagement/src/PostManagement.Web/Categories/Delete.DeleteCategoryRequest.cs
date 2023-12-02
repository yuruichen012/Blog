namespace PostManagement.Web.Categories;

public class DeleteCategoryRequest
{
    public const string Route = "/Category/{Id}";

    public Guid Id { get; set; }
}
