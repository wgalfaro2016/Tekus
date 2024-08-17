using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Queries.CustomProviderField
{
    public class GetCustomProviderFieldByIdQuery : IRequest<CustomerProviderServiceDto>
    {
        public int ID { get; set; }
    }

    public class GetProviderByIdQueryHandler : IRequestHandler<GetCustomProviderFieldByIdQuery, CustomerProviderServiceDto>
    {
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public GetProviderByIdQueryHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerProviderServiceDto> Handle(GetCustomProviderFieldByIdQuery request, CancellationToken cancellationToken) {
            var customProvider = await _context.CustomProviderFields.FindAsync(request.ID);
            return _mapper.Map<CustomerProviderServiceDto>(customProvider);
        }
    }
}