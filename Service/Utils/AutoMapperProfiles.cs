using AutoMapper;
using Database.Models;
using Service.Dtos.Auth;
using Service.Dtos.User;

namespace Service.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<SignupDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserWithTokenDto>();
        }
    }
}
