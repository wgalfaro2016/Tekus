using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Queries.CustomProviderField
{
    public class GetProviderWithCustomFieldsQuery : IRequest<ProviderWithCustomFieldsDto>
    {
        public int ID { get; set; }
    }

    public class GetProviderWithCustomFieldsQueryHandler : IRequestHandler<GetProviderWithCustomFieldsQuery, ProviderWithCustomFieldsDto>
    {
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public GetProviderWithCustomFieldsQueryHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderWithCustomFieldsDto> Handle(GetProviderWithCustomFieldsQuery request, CancellationToken cancellationToken) {
            var provider = await _context.Providers
                .Include(p => p.CustomProviderFields) 
                .FirstOrDefaultAsync(p => p.ProviderID == request.ID, cancellationToken);

            if (provider == null) {
                return null;
            }

            return _mapper.Map<ProviderWithCustomFieldsDto>(provider);
        }
    }
}
