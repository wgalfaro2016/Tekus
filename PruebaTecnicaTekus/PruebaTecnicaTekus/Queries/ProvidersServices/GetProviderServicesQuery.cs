using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Queries.ProvidersServices
{
    public class GetProviderServicesQuery : IRequest<List<ProviderServiceDto>>
    {
    }
    public class GetProviderServicesQueryHandler : IRequestHandler<GetProviderServicesQuery, List<ProviderServiceDto>>
    {
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public GetProviderServicesQueryHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProviderServiceDto>> Handle(GetProviderServicesQuery request, CancellationToken cancellationToken) {
            var providerServices = await _context.ProviderServices.ToListAsync(cancellationToken);
            return _mapper.Map<List<ProviderServiceDto>>(providerServices);
        }
    }

}
