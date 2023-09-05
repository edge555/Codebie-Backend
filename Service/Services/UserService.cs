using AutoMapper;
using Database.Models;
using Repository.Interfaces;
using Service.CustomExceptions;
using Service.Dtos.User;
using Service.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IDateTimeHandler _dateTimeHandler;
        private readonly IPasswordHandler _passwordHandler;
        public UserService(IUserRepository userRepository, IMapper mapper, IJwtTokenHandler jwtTokenHandler, IDateTimeHandler dateTimeHandler, IPasswordHandler passwordHandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenHandler = jwtTokenHandler;
            _dateTimeHandler = dateTimeHandler;
            _passwordHandler = passwordHandler;
        }
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            List<User> users = await _userRepository.GetUsersAsync();
            IEnumerable<UserDto> userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> UpdateUserByIdAsync(string userId, UserUpdateDto userUpdateDto)
        {
            if (_jwtTokenHandler.IsTokenExpired())
            {
                throw new UnauthorizedException("Token expired, Please log in again.");
            }
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("No user found with this id");
            }
            var loggedInUserId = _jwtTokenHandler.GetLoggedInUserId();
            if (!string.Equals(loggedInUserId, userId, StringComparison.OrdinalIgnoreCase))
            {
                throw new ForbiddenException("You do not have permission to perform this action.");
            }
            if(userUpdateDto.Password != null)
            {
                userUpdateDto.Password = _passwordHandler.HashPassword(userUpdateDto.Password);
            }
            var userData = _mapper.Map<User>(userUpdateDto);
            userData.UpdatedAt = _dateTimeHandler.GetCurrentUtcTime();
            var updatedUser = await _userRepository.UpdateUserByIdAsync(userId, userData);
            var userDto = _mapper.Map<UserDto>(updatedUser);
            return userDto;
        }

        public async Task<Boolean> DeleteUserByIdAsync(string userId)
        {
            if (_jwtTokenHandler.IsTokenExpired())
            {
                throw new UnauthorizedException("Token expired, Please log in again.");
            }
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("No user found with this id");
            }
            var loggedInUserId = _jwtTokenHandler.GetLoggedInUserId();
            if (loggedInUserId != userId)
            {
                throw new ForbiddenException("You do not have permission to perform this action.");
            }
            return await _userRepository.DeleteUserByIdAsync(userId);
        }
    }
}
