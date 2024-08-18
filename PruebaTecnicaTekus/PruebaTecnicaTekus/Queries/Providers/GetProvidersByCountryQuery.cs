using MediatR;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.Providers;

namespace PruebaTecnicaTekus.Queries.Providers
{
    public class GetProvidersByCountryQuery : IRequest<List<ProvidersByCountryDto>>
    {
        public class GetProvidersByCountryQueryHandler : IRequestHandler<GetProvidersByCountryQuery, List<ProvidersByCountryDto>>
        {
            private readonly IProvidersRepository _providerRepository;

            public GetProvidersByCountryQueryHandler(IProvidersRepository providerRepository) {
                _providerRepository = providerRepository;
            }

            public async Task<List<ProvidersByCountryDto>> Handle(GetProvidersByCountryQuery request, CancellationToken cancellationToken) {

                return await _providerRepository.GetProvidersByCountry();
            }
        }
    }
}
