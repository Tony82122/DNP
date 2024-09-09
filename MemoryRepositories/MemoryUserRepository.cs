using Entities;
using EntityRepository;

namespace Server;
public class InMemoryUserRepository : IUserRepo
{
    private readonly List<User> _users = new();

    public Task<User> AddAsync(User user)
    {
        user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
            throw new InvalidOperationException("User not found");

        _users.Remove(existingUser);
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        _users.Remove(user);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        return Task.FromResult(user);
    }

    public IQueryable<User> GetAll()
    {
        return _users.AsQueryable();
    }

    public Task<IQueryable<User>> GetManyAsync()
    {
        if (!_users.Any())
        {
            throw new InvalidOperationException("No users found");
        }

        return Task.FromResult(_users.AsQueryable());
    }
}