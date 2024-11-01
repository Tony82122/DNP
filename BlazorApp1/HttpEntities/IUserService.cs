using System.Threading.Tasks;
using ApiContracts;

namespace BlazorApp1.HttpServices
{
    public interface IUserService
    {
       // Task<UserDto> AddUserAsync(CreateUserDto request);
        //Task<UserDto> GetUserAsync(int id);
        //Task UpdateUserAsync(int id, UpdateUserDto request);
        Task DeleteUserAsync(int id);
        // Add other methods as needed
    }
}