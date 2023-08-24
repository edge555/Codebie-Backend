using Service.Dtos.Auth;
using Service.Dtos.User;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> SignupAsync(SignupDto request);
    }
}
