using AutoMapper;
using TaskManager.Application.Common.Dtos;

namespace TaskManager.Application.Common.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskManager.Domain.Task, TaskDto>()
                .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.User!.Name)
                );
        }
    }
}
