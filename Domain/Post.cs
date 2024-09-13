namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public int Upvotes { get; set; } 
    public string? Content { get; set; }
    public int Downvotes { get; set; }

}