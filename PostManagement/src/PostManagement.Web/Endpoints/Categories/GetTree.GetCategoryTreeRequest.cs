namespace PostManagement.Web.Endpoints.Categories;

public record class GetCategoryTreeRequest(int Id)
{
    public const string Route = "/Category/{Id}/Tree";
}
