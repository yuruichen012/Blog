namespace PostManagement.Web.Endpoints.Categories;

public record class CreateCategoryRequest(int ParentId, string Name)
{
    public static readonly string Route = "/Category";
}
