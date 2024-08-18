using MediatR;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.Services;

namespace PruebaTecnicaTekus.Queries.Services
{
    public class GetServicesByCountryQuery : IRequest<List<ServicesByCountryDto>>
    {
        public class GetServicesByCountryQueryHandler : IRequestHandler<GetServicesByCountryQuery, List<ServicesByCountryDto>>
        {
            private readonly IServiceRepository _serviceRepository;

            public GetServicesByCountryQueryHandler(IServiceRepository serviceRepository) 
            {
                _serviceRepository = serviceRepository;
            }

            public async Task<List<ServicesByCountryDto>> Handle(GetServicesByCountryQuery request, CancellationToken cancellationToken) {

                return await _serviceRepository.GetServicesByCountryAsync();
            }
        }
    }
}
