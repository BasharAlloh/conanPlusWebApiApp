using AutoMapper;
using conanPlusWebApiApp.DTOs;
using conanPlusWebApiApp.Models;

namespace conanPlusWebApiApp.Profiles
{
    public class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<PartnerCreateDTO, Partner>();
            CreateMap<PartnerUpdateDTO, Partner>();
            CreateMap<Partner, PartnerDTO>();

        }
    }

}
