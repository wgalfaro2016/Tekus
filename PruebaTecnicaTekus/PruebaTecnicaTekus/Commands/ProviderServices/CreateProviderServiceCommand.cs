using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Repositories.ProviderServices;
using PruebaTecnicaTekus.Response.ProvidersService;

namespace PruebaTecnicaTekus.Commands.ProviderServices
{
    public class CreateProviderServiceCommand : IRequest<ProviderServiceResponse>
    {
        public int ProviderId { get; set; }
        public int ServiceId { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class CreateProviderServiceCommandHandler : IRequestHandler<CreateProviderServiceCommand, ProviderServiceResponse>
    {
        private readonly IProviderServicesRepository _providerServicesRepository;
        private readonly IMapper _mapper;

        public CreateProviderServiceCommandHandler(IProviderServicesRepository providerServicesRepository, IMapper mapper) 
        {
            _providerServicesRepository = providerServicesRepository;
            _mapper = mapper;
        }

        public async Task<ProviderServiceResponse> Handle(CreateProviderServiceCommand request, CancellationToken cancellationToken) {
            var providerService = new ProviderService {
                ProviderID = request.ProviderId,
                ServiceID = request.ServiceId,
                StartDate = request.StartDate
            };

            var result = await _providerServicesRepository.AddAsync(providerService);

            if (result > 0) {
                return new ProviderServiceResponse {
                    IsSuccess = true,
                    ProviderServiceId = providerService.ProviderServiceID
                };
            }

            return new ProviderServiceResponse {
                IsSuccess = false,
                ProviderServiceId = null,
                ErrorMessage = $"There was a problem inserting a provider service"
            };
        }
    }
}
