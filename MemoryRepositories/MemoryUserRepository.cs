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
        if (existingUser == null || existingUser.password!= user.password)
            throw new InvalidOperationException("User not found");

        existingUser.UserName = user.UserName;
        existingUser.password = user.password;
        existingUser.Email = user.Email;
        existingUser.Joined = user.Joined;
        existingUser.Subscribes = user.Subscribes;

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

    public Task<User> SubscribeAsync(int userId, int postId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (!user.Subscribes.Contains(postId))
        {
            user.Subscribes.Add(postId);
        }

        return Task.FromResult(user);
    }

    public Task<User> UnsubscribeAsync(int userId, int postId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (user.Subscribes.Contains(postId))
        {
            user.Subscribes.Remove(postId);
        }

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