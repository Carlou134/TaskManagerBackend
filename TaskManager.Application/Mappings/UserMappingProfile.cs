using AutoMapper;
using TaskManager.Application.Users.Dtos;
using TaskManager.Domain;

namespace TaskManager.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
