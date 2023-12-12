namespace PostManagement.Web.Endpoints.Posts;

public record class CreatePostRequest(string Title, string Content, int CategoryId)
{
    public static readonly string Route = "/Post";
}
