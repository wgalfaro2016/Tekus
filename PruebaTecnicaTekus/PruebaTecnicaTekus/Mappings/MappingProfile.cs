using AutoMapper;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Provider, ProviderDto>();
            CreateMap<ProviderService, ProviderServiceDto>();
            CreateMap<ProviderDto, Provider>();

            CreateMap<Provider, ProviderWithCustomFieldsDto>()
                .ForMember(dest => dest.CustomProviderFields, opt => opt.MapFrom(src => src.CustomProviderFields));

            CreateMap<ProviderWithCustomFieldsDto, Provider>()
                .ForMember(dest => dest.CustomProviderFields, opt => opt.MapFrom(src => src.CustomProviderFields));

            CreateMap<CustomProviderField, CustomFieldDto>();
            CreateMap<CustomFieldDto, CustomProviderField>();
        }
    }
}