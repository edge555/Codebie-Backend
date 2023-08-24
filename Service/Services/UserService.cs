using AutoMapper;
using Database.Models;
using Repository.Interfaces;
using Service.Dtos.User;
using Service.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            List<User> users = await _userRepository.GetUsersAsync();
            IEnumerable<UserDto> userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
        public async Task<UserDto> GetUserByGuIdAsync(string GuId)
        {
            var user = await _userRepository.GetUserByGuIdAsync(GuId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<UserDto> GetUserByUsernameAsync(string Username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(Username);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

    }
}
