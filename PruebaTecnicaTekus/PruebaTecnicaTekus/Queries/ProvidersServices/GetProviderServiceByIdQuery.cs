using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Queries.ProvidersServices
{
    public class GetProviderServiceByIdQuery : IRequest<ProviderServiceDto>
    {
        public int Id { get; set; }
    }

    public class GetProviderServiceByIdQueryHandler : IRequestHandler<GetProviderServiceByIdQuery, ProviderServiceDto>
    {
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public GetProviderServiceByIdQueryHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderServiceDto> Handle(GetProviderServiceByIdQuery request, CancellationToken cancellationToken) {
            var providerService = await _context.ProviderServices
                .FirstOrDefaultAsync(ps => ps.ProviderServiceID == request.Id, cancellationToken);

            if (providerService == null) {
                return null;
            }

            return _mapper.Map<ProviderServiceDto>(providerService);
        }
    }
}
