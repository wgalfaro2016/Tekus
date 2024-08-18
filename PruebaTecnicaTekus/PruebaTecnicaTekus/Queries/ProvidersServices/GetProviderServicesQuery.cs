using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.ProviderServices;

namespace PruebaTecnicaTekus.Queries.ProvidersServices
{
    public class GetProviderServicesQuery : IRequest<List<ProviderServiceDto>>
    {
    }
    public class GetProviderServicesQueryHandler : IRequestHandler<GetProviderServicesQuery, List<ProviderServiceDto>>
    {
        private readonly IProviderServicesRepository _providerServicesRepository;
        private readonly IMapper _mapper;

        public GetProviderServicesQueryHandler(IProviderServicesRepository providerServicesRepository, IMapper mapper)
        {
            _providerServicesRepository = providerServicesRepository;
            _mapper = mapper;
        }

        public async Task<List<ProviderServiceDto>> Handle(GetProviderServicesQuery request, CancellationToken cancellationToken) {
            var providerServices = await _providerServicesRepository.GetProvidersServiceListAsync();
            return _mapper.Map<List<ProviderServiceDto>>(providerServices);
        }
    }

}
