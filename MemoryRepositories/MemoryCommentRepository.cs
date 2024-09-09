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

        comments.Remove(existingComment);
        comments.Add(comment);
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

    public IQueryable<Comment> GetManyAsync()
    {
        if (! comments.Any())
        {
            throw new InvalidOperationException("No comments found");
        }

        return comments.AsQueryable();

    }
}