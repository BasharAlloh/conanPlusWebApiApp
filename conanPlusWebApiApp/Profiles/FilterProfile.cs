using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class FilterProfile : Profile
    {
        public FilterProfile()
        {
            // Mapping for Create and Update
            CreateMap<FilterCreateDTO, Filter>();
            CreateMap<FilterUpdateDTO, Filter>();

            // Mapping for Display (including Service name)
            CreateMap<Filter, FilterDisplayDTO>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
                .ForMember(dest => dest.ProjectNames, opt => opt.MapFrom(src => src.Projects.Select(p => p.ProjectTitle)));
        }
    }
}
