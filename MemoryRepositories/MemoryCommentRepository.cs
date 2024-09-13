using Entities;
using EntityRepository;

namespace Server;

public class InMemoryCommentRepository : ICommentRepository
{
    private readonly List<Comment> comments = new List<Comment>();

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Max(c => c.Id) + 1 : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        var existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);
        if (existingComment == null)
            throw new InvalidOperationException("Comment not found");

        existingComment.PostId = comment.PostId;
        existingComment.Body = comment.Body;
        existingComment.Upvotes = comment.Upvotes;
        existingComment.Downvotes = comment.Downvotes;
        existingComment.CreatedAt = comment.CreatedAt;
        existingComment.UserId = comment.UserId;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
            throw new InvalidOperationException("Comment not found");

        comments.Remove(comment);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
            throw new InvalidOperationException("Comment not found");

        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetManyAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task UpvoteAsync(int commentId)
    {
        var comment = comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
        {
            throw new InvalidOperationException("Oops!!");
        }
        comment.Upvotes++;
        return Task.CompletedTask;
    }
    
    public Task DownVoteAsync(int commentId)
    {
        var comment = comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
        {
            throw new InvalidOperationException("Not found" + commentId);
        }
        comment.Downvotes++;
        return Task.CompletedTask;
    }

    public Task<TimeSpan> DateTimeAgoAsync(int commentId)
    {
        var comment = comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        var timeAgo = DateTime.UtcNow - comment.CreatedAt;
        return Task.FromResult(timeAgo);
    }

    public Task<int> UserDetailsAsync(int commentId)
    {
        var comment = comments.FirstOrDefault(c => c.Id == commentId);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        return Task.FromResult(comment.UserId);
    }
    
    public InMemoryCommentRepository()
    {
        comments.Add(new Comment { Id = 1, PostId = 1, UserId = 1, Body = "yeah it was Trash", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 2, PostId = 1, UserId = 2, Body = "I liked playing the game", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 3, PostId = 2, UserId = 3, Body = "Nope, not worth it", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 4, PostId = 3, UserId = 4, Body = "This is amazing", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 5, PostId = 3, UserId = 5, Body = "I agree", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 6, PostId = 4, UserId = 6, Body = "No, I don't like it", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 7, PostId = 4, UserId = 7, Body = "I agree too", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 8, PostId = 5, UserId = 8, Body = "I love it", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 9, PostId = 5, UserId = 9, Body = "I agree", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 10, PostId = 5, UserId = 10, Body = "I disagree", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 11, PostId = 6, UserId = 11, Body = "I hate it", CreatedAt = DateTime.UtcNow });
        comments.Add(new Comment { Id = 12, PostId = 6, UserId = 12, Body = "I agree 10/10", CreatedAt = DateTime.UtcNow });
        
    }
    
    public IQueryable<Comment> GetManyAsync()
    {
        if (!comments.Any())
        {
            throw new InvalidOperationException("No comments found");
        }

        return comments.AsQueryable();
    }
}