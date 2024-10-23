using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Create and Update Mapping
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>();

            // Display Mapping
            CreateMap<Employee, EmployeeDisplayDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString())); 

            // General Mapping
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
