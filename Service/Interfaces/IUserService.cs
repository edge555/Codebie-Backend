using Service.Dtos.User;

namespace Service.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsersAsync();
        public Task<UserDto> GetUserByIdAsync(string userId);
        public Task<UserDto> GetUserByUsernameAsync(string Username);
        public Task<UserDto> UpdateUserByIdAsync(string userId, UserUpdateDto userUpdateDto);
        public Task<Boolean> DeleteUserByIdAsync(string userId);
    }
}
