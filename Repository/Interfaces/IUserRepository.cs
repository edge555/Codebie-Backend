using Database.Models;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByGuIdAsync(string GuId);
        Task<User> GetUserByUsernameAsync(string Username);
        Task<User> GetUserByEmailAsync(string Email);
    }
}
