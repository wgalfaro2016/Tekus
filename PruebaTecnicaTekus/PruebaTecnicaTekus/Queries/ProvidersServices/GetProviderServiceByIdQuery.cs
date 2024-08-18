using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.ProviderServices;

namespace PruebaTecnicaTekus.Queries.ProvidersServices
{
    public class GetProviderServiceByIdQuery : IRequest<ProviderServiceDto>
    {
        public int Id { get; set; }
    }

    public class GetProviderServiceByIdQueryHandler : IRequestHandler<GetProviderServiceByIdQuery, ProviderServiceDto>
    {
        private readonly IProviderServicesRepository _providerServicesRepository;
        private readonly IMapper _mapper;

        public GetProviderServiceByIdQueryHandler(IProviderServicesRepository providerServicesRepository, IMapper mapper) {
            _providerServicesRepository = providerServicesRepository;
            _mapper = mapper;
        }

        public async Task<ProviderServiceDto> Handle(GetProviderServiceByIdQuery request, CancellationToken cancellationToken) {
            var providerService = await _providerServicesRepository.GetByIdAsync(request.Id);

            if (providerService == null) {
                return null;
            }

            return _mapper.Map<ProviderServiceDto>(providerService);
        }
    }
}
