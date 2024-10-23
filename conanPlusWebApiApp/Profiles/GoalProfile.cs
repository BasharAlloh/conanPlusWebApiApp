using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class GoalProfile : Profile
    {
        public GoalProfile()
        {
            CreateMap<GoalCreateDTO, Goal>();
            CreateMap<GoalUpdateDTO, Goal>();
            CreateMap<Goal, GoalDTO>();
        }
    }
}
