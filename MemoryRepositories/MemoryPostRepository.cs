using Entities;
using EntityRepository;

namespace Server;

public class InMemoryPostRepository : IPostRepo
{
    private readonly List<Post> posts = new List<Post>();

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        var existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
            throw new InvalidOperationException("Post not found");

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
        existingPost.body = post.body;
        existingPost.UserId = post.UserId;
        existingPost.Upvotes = post.Upvotes;
        existingPost.Downvotes = post.Downvotes;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var post = posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
            throw new InvalidOperationException("Post not found");

        posts.Remove(post);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        var post = posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
            throw new InvalidOperationException("Post not found");

        return Task.FromResult(post);
    }

    public Task UpvoteAsync(int postId)
    {
        var post = posts.FirstOrDefault(p => p.Id == postId);
        if (post == null)
        {
            throw new InvalidOperationException("Post not found");
        }
        post.Upvotes++;
        return Task.CompletedTask;
    }
    
    public Task DownvoteAsync(int postId)
    {
        var post = posts.FirstOrDefault(p => p.Id == postId);
        if (post == null)
        {
            throw new InvalidOperationException("Post not found");
        }
        post.Downvotes++;
        return Task.CompletedTask;
    }

    public IQueryable<Post> GetAll()
    {
        if (!posts.Any())
        {
            throw new InvalidOperationException("No posts found");
        }

        return posts.AsQueryable();
    }

    public IQueryable<Post> GetManyAsync()
    {
        if (!posts.Any())
        {
            throw new InvalidOperationException("No posts found");
        }

        return posts.AsQueryable();
    }
}