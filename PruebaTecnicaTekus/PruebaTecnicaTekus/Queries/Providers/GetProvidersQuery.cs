using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.Providers;

namespace PruebaTecnicaTekus.Queries.Providers
{
    public class GetProvidersQuery : IRequest<List<ProviderDto>>
    {
    }

    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, List<ProviderDto>>
    {
        private readonly IProvidersRepository _providersRepository;
        private readonly IMapper _mapper;

        public GetProvidersQueryHandler(IProvidersRepository providersRepository, IMapper mapper)
        {
            _providersRepository = providersRepository;
            _mapper = mapper;
        }

        public async Task<List<ProviderDto>> Handle(GetProvidersQuery request, CancellationToken cancellationToken) {
            var providers = await _providersRepository.GetProvidersListAsync();
            return _mapper.Map<List<ProviderDto>>(providers);
        }
    }
}
