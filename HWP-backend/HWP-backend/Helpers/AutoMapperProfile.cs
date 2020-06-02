using System.Linq;
using AutoMapper;
using HWP_backend.Entities;
using HWP_backend.Models.DTO.SolvedTasks;
using HWP_backend.Models.DTO.Tasks;
using HWP_backend.Models.DTO.Users;

namespace HWP_backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User
            CreateMap<User, UserModelDTO>()
                .ForMember(x => x.Tasks,
                    opt => opt
                        .MapFrom(x => x.SolvedTasks
                            .Select(tasks => tasks.Task))).ReverseMap();

            CreateMap<RegisterModelDTO, User>().ReverseMap();
            CreateMap<UpdateModelDTO, User>().ReverseMap();

            //Task
            CreateMap<Task, TaskModelDTO>()
                .ReverseMap();
            CreateMap<CreateTaskModelDTO, Task>().ReverseMap();
            CreateMap<UpdateTaskModelDTO, Task>().ReverseMap();

            //SolvedTask
            CreateMap<SolvedTask, SolvedTaskDTO>().ReverseMap();
            CreateMap<CreateSolvedTaskDTO, SolvedTask>().ReverseMap();
            CreateMap<RateSolvedTaskDTO, SolvedTask>().ReverseMap();
            CreateMap<UpdateSolvedTaskDTO, SolvedTask>().ReverseMap();
        }
    }
}