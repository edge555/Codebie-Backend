using Service.Dtos.User;

namespace Service.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsersAsync();
        public Task<UserDto> GetUserByGuIdAsync(string GuId);
        public Task<UserDto> GetUserByUsernameAsync(string Username);
    }
}
