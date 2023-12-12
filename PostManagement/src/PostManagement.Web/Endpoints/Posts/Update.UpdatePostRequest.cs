namespace PostManagement.Web.Endpoints.Posts;

public record class UpdatePostRequest(int Id, string Title, string Content)
{
    public static readonly string Route = "/Post/{Id}";
}
