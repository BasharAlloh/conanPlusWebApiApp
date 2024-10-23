using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class VisionProfile : Profile
    {
        public VisionProfile()
        {
            CreateMap<Vision, VisionDTO>();
            CreateMap<VisionCreateDTO, Vision>();
            CreateMap<VisionUpdateDTO, Vision>();
        }
    }
}
