using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.ExternalImageUrl, opt => opt.Ignore()) 
                .ForMember(dest => dest.InternalImageUrl, opt => opt.Ignore());

            CreateMap<ProjectUpdateDTO, Project>()
                .ForMember(dest => dest.ExternalImageUrl, opt => opt.Ignore())  
                .ForMember(dest => dest.InternalImageUrl, opt => opt.Ignore());

            CreateMap<Project, ProjectDisplayDTO>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
                .ForMember(dest => dest.FilterName, opt => opt.MapFrom(src => src.Filter.FilterName));
        }
    }
}
