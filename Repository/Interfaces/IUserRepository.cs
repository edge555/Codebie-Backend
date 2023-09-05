using Database.Models;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User?>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(string UserId);
        Task<User?> GetUserByUsernameAsync(string Username);
        Task<User?> GetUserByEmailAsync(string Email);
        Task<User?> UpdateUserByIdAsync(string UserId, User user);
        Task<Boolean> DeleteUserByIdAsync(string UserId);
    }
}
