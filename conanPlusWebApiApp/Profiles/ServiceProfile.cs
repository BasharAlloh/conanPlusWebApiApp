using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // Map Service to ServiceDisplayDTO with all related entities
        CreateMap<Service, ServiceDisplayDTO>()
            .ForMember(dest => dest.Filters, opt => opt.MapFrom(src => src.Filters))
            .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        // Mapping for update operations
        CreateMap<ServiceUpdateDTO, Service>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.ServiceName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        // Mapping for related DTOs
        CreateMap<Filter, FilterDTO>();
        CreateMap<Project, ProjectDisplayDTO>(); // استبدال ProjectDTO بـ ProjectDisplayDTO
    }
}
