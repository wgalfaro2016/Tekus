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
        }
    }
}