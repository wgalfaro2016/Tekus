using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Repositories.Providers;

namespace PruebaTecnicaTekus.Queries.CustomProviderField
{
    public class GetProviderWithCustomFieldsQuery : IRequest<ProviderWithCustomFieldsDto>
    {
        public int ID { get; set; }
    }

    public class GetProviderWithCustomFieldsQueryHandler : IRequestHandler<GetProviderWithCustomFieldsQuery, ProviderWithCustomFieldsDto>
    {
        private readonly IProvidersRepository _providersRepository;
        private readonly IMapper _mapper;

        public GetProviderWithCustomFieldsQueryHandler(IProvidersRepository providersRepository, IMapper mapper) 
        {
            _providersRepository = providersRepository;
            _mapper = mapper;
        }

        public async Task<ProviderWithCustomFieldsDto> Handle(GetProviderWithCustomFieldsQuery request, CancellationToken cancellationToken) {
            var provider = await _providersRepository.GetProviderWithCustomFieldsAsync(request.ID);

            if (provider == null) {
                return null;
            }

            return _mapper.Map<ProviderWithCustomFieldsDto>(provider);
        }
    }
}
