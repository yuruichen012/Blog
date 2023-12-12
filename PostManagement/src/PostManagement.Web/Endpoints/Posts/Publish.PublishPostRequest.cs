namespace PostManagement.Web.Endpoints.Posts;

public record class PublishPostRequest(int Id)
{
    public static readonly string Route = "/Post/{Id}/Publish";
}
