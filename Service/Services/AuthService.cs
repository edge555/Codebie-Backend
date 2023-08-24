using AutoMapper;
using Database.Models;
using Repository.Interfaces;
using Service.Dtos.Auth;
using Service.Dtos.User;
using Service.Interfaces;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IUserRepository userRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> SignupAsync(SignupDto request)
        {
            var user = _mapper.Map<User>(request);
            user.Password = request.Password;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.PasswordModifiedAt = DateTime.UtcNow;
            var newUser = await _authRepository.PostUserAsync(user);
            
            var userDto = _mapper.Map<UserDto>(newUser);
            return userDto;
        }
    }
}
