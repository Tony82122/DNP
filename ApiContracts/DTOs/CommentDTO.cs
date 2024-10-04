namespace ApiContracts.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Upvotes { get; set; } = 0;
    public int Downvotes { get; set; } = 0;
    public string? TextContent { get; set; }
}