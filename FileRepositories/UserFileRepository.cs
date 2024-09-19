using System.Text.Json;
using Entities;
using EntityRepository;

namespace FileRepositories;

public class UserFileRepository: IUserRepo
{
    private readonly string filePath = "users.json";
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public Task<User> AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<User>> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}