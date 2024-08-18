using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.CustomProviderFields;
using PruebaTecnicaTekus.Repositories.Providers;

namespace PruebaTecnicaTekus.Queries.CustomProviderField
{
    public class GetCustomProviderFieldByIdQuery : IRequest<ProviderWithCustomFieldsDto>
    {
        public int ID { get; set; }
    }

    public class GetProviderByIdQueryHandler : IRequestHandler<GetCustomProviderFieldByIdQuery, ProviderWithCustomFieldsDto>
    {
        private readonly IMapper _mapper;
        private readonly IProvidersRepository _repository;

        public GetProviderByIdQueryHandler(TekusContext context, IMapper mapper, IProvidersRepository repository) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ProviderWithCustomFieldsDto> Handle(GetCustomProviderFieldByIdQuery request, CancellationToken cancellationToken) {
            var customProvider = await _repository.GetProviderWithCustomFieldsAsync(request.ID);
            return _mapper.Map<ProviderWithCustomFieldsDto>(customProvider);
        }
    }
}