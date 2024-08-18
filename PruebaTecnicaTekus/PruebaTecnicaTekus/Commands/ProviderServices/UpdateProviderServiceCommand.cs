using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Repositories.ProviderServices;
using PruebaTecnicaTekus.Response.Providers;
using PruebaTecnicaTekus.Response.ProvidersService;

namespace PruebaTecnicaTekus.Commands.ProviderServices
{
    public class UpdateProviderServiceCommand : IRequest<ProviderServiceResponse>
    {
        public int ProviderServiceId { get; set; }
        public int ProviderId { get; set; }
        public int ServiceId { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class UpdateProviderServiceCommandHandler : IRequestHandler<UpdateProviderServiceCommand, ProviderServiceResponse>
    {
        private readonly IProviderServicesRepository _providerServicesRepository;
        private readonly IMapper _mapper;

        public UpdateProviderServiceCommandHandler(IProviderServicesRepository providerServicesRepository, IMapper mapper) 
        {
            _providerServicesRepository = providerServicesRepository;
            _mapper = mapper;
        }

        public async Task<ProviderServiceResponse> Handle(UpdateProviderServiceCommand request, CancellationToken cancellationToken) {
            var providerService = await _providerServicesRepository.GetByIdAsync(request.ProviderServiceId);

            if (providerService == null) {
                return new ProviderServiceResponse {
                    IsSuccess = false,
                    ProviderServiceId = null,
                    ErrorMessage = $"Provider service {request.ProviderServiceId} doesn't exists"
                };
            }

            providerService.ProviderID = request.ProviderId;
            providerService.ServiceID = request.ServiceId;
            providerService.StartDate = request.StartDate;

            var result = await _providerServicesRepository.UpdateAsync(providerService);

            if (result > 0) {
                return new ProviderServiceResponse {
                    IsSuccess = true,
                    ProviderServiceId = providerService.ProviderServiceID
                };
            }

            return new ProviderServiceResponse {
                IsSuccess = false,
                ProviderServiceId = null,
                ErrorMessage = $"There was a problem updating the provider service - {request.ProviderServiceId}"
            };
        }
    }
}