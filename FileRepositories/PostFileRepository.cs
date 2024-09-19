using System.Text.Json;
using Entities;
using EntityRepository;

namespace FileRepositories;

public class PostFileRepository: IPostRepo
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public Task<Post> AddAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}