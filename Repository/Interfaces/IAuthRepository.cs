using Database.Models;

namespace Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> PostUserAsync(User user);
    }
}
