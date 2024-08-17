using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Queries.Providers
{
    public class GetProvidersQuery : IRequest<List<ProviderDto>>
    {
    }

    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, List<ProviderDto>>
    {
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public GetProvidersQueryHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProviderDto>> Handle(GetProvidersQuery request, CancellationToken cancellationToken) {
            var providers = await _context.Providers.ToListAsync(cancellationToken);
            return _mapper.Map<List<ProviderDto>>(providers);
        }
    }
}
