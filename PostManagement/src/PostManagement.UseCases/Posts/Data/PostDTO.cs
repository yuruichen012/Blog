using PostManagement.Core.PostAggregates;

namespace PostManagement.UseCases.Posts.Data;

public record class PostDTO(int Id, string Title, string Content, Guid AuthorId, string AuthorName, int Status, DateTime? PublishedTime)
{
    public static implicit operator PostDTO(Post post)
    {
        return new PostDTO(post.Id, post.Title, post.Content, post.Author.Id, post.Author.Name, post.Status.Value, post.PublishedTime);
    }
}
