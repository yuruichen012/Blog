namespace PostManagement.Web.Categories;

public record class UpdateCategoryRequest(int Id, int ParentId, string Name)
{
    public static readonly string Route = "/Category/{Id}";
}
