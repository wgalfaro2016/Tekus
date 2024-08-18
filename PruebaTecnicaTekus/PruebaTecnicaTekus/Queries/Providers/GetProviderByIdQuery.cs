using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.Providers;

namespace PruebaTecnicaTekus.Queries.Providers
{
    public class GetProviderByIdQuery : IRequest<ProviderDto>
    {
        public int ProviderID { get; set; }
    }

    public class GetProviderByIdQueryHandler : IRequestHandler<GetProviderByIdQuery, ProviderDto>
    {
        private readonly IProvidersRepository _providersRepository;
        private readonly IMapper _mapper;

        public GetProviderByIdQueryHandler(IProvidersRepository providersRepository, IMapper mapper) {
            _providersRepository = providersRepository;
            _mapper = mapper;
        }

        public async Task<ProviderDto> Handle(GetProviderByIdQuery request, CancellationToken cancellationToken) {
            var provider = await _providersRepository.GetByIdAsync(request.ProviderID);
            return _mapper.Map<ProviderDto>(provider);
        }
    }

}
