namespace PostManagement.Web.Categories;

public class GetByIdCategoryRequest(uint id)
{
    public const string Route = "/Category/{Id}";

    public uint Id { get; init; } = id;
}
