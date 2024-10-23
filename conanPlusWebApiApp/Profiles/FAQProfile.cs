using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class FAQProfile : Profile
    {
        public FAQProfile()
        {
            CreateMap<FAQCreateDTO, FAQ>();
            CreateMap<FAQUpdateDTO, FAQ>();
            CreateMap<FAQ, FAQDisplayDTO>();
        }
    }
}
