using Entities;
using EntityRepository;

namespace Server;
public class InMemoryUserRepository : IUserRepo
{
    private readonly List<User> users = new();

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
            throw new InvalidOperationException("User not found");
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        users.Remove(user);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        return Task.FromResult(user);
    }

    public IQueryable<User> GetAll()
    {
        return users.AsQueryable();
    }

    public Task<IQueryable<User>> GetManyAsync()
    {
        if (!users.Any())
        {
            throw new InvalidOperationException("No users found");
        }

        return Task.FromResult(users.AsQueryable());
    }
}