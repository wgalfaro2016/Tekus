using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Queries.Providers
{
    public class GetProviderByIdQuery : IRequest<ProviderDto>
    {
        public int ProviderID { get; set; }
    }

    public class GetProviderByIdQueryHandler : IRequestHandler<GetProviderByIdQuery, ProviderDto>
    {
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public GetProviderByIdQueryHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderDto> Handle(GetProviderByIdQuery request, CancellationToken cancellationToken) {
            var provider = await _context.Providers.FindAsync(request.ProviderID);
            return _mapper.Map<ProviderDto>(provider);
        }
    }

}
