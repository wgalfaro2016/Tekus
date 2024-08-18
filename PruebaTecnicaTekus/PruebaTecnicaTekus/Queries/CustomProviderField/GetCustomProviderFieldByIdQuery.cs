using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.CustomProviderFields;

namespace PruebaTecnicaTekus.Queries.CustomProviderField
{
    public class GetCustomProviderFieldByIdQuery : IRequest<CustomerProviderServiceDto>
    {
        public int ID { get; set; }
    }

    public class GetProviderByIdQueryHandler : IRequestHandler<GetCustomProviderFieldByIdQuery, CustomerProviderServiceDto>
    {
        private readonly IMapper _mapper;
        private readonly ICustomProviderFieldRepository _repository;

        public GetProviderByIdQueryHandler(TekusContext context, IMapper mapper, ICustomProviderFieldRepository repository) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomerProviderServiceDto> Handle(GetCustomProviderFieldByIdQuery request, CancellationToken cancellationToken) {
            var customProvider = await _repository.GetByIdAsync(request.ID);
            return _mapper.Map<CustomerProviderServiceDto>(customProvider);
        }
    }
}