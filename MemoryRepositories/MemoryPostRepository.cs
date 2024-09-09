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
            throw new InvalidOperationException("Oops couldn't find post");

        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var post = posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
            throw new InvalidOperationException("Post Doesn't exist");

        posts.Remove(post);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        var post = posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
            throw new InvalidOperationException("Post not found!!");

        return Task.FromResult(post);
    }

    public IQueryable<Post> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetManyASync()
    {
        if (!posts.Any())
        {
            throw new InvalidOperationException("No posts found");
        }

        return posts.AsQueryable();
    }
}