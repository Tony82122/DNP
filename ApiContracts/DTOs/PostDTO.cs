namespace ApiContracts.DTOs;

public class PostDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    
    public int? UpVotes { get; set; }
    
    public int? DownVotes { get; set; }
    
    public string? Content { get; set; }
}