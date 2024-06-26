using AutoMapper;
using InsuranceWebAPI.Models.Dto;
using InsuranceWebAPI.Models;
using InsuraceWebAPI.Models.Dto;
namespace InsuranceWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InsurancePolicy, InsurancePolicyDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserWithInsurancePoliciesDto, User>().ReverseMap();
            CreateMap<User, UserWithInsurancePoliciesDto>()
               .ForMember(dest => dest.InsurancePolicies, opt => opt.MapFrom(src => src.InsurancePolicies));

        }
    }

}
