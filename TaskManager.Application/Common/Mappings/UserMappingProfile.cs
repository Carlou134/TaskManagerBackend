using AutoMapper;
using TaskManager.Application.Common.Dtos;
using TaskManager.Domain;

namespace TaskManager.Application.Common.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
