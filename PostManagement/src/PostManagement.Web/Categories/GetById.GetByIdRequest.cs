namespace PostManagement.Web.Categories;

public class GetByIdCategoryRequest(Guid id)
{
    public const string Route = "/Category/{Id}";

    public Guid Id { get; init; } = id;
}
