using Entities;

namespace EntityRepository;

public interface IUserRepo
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    
    IQueryable<User> GetAll();
    Task<IQueryable<User>> GetManyAsync();
}