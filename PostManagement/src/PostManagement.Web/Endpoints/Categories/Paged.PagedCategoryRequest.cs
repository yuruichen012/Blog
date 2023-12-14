namespace PostManagement.Web.Endpoints.Categories;

public record class PagedCategoryRequest(int PageNumber = 1, int PageSize = 10)
{
    public static readonly string Route = "/Categories";
}
