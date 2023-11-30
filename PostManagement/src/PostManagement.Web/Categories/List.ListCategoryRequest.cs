namespace PostManagement.Web.Categories;

public class ListCategoryRequest(int skip = 0, int take = 10)
{
    public const string Route = "/Categories";

    public int Skip { get; init; } = skip;

    public int Take { get; init; } = take;
}
