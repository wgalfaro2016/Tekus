using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
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
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public UpdateProviderServiceCommandHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderServiceResponse> Handle(UpdateProviderServiceCommand request, CancellationToken cancellationToken) {
            var providerService = await _context.ProviderServices.FindAsync(request.ProviderServiceId);

            if (providerService == null) {
                return new ProviderServiceResponse {
                    IsSuccess = false,
                    ProviderServiceId = null
                };
            }

            providerService.ProviderID = request.ProviderId;
            providerService.ServiceID = request.ServiceId;
            providerService.StartDate = request.StartDate;

            _context.ProviderServices.Update(providerService);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) {
                return new ProviderServiceResponse {
                    IsSuccess = true,
                    ProviderServiceId = providerService.ProviderServiceID
                };
            }

            return new ProviderServiceResponse {
                IsSuccess = false,
                ProviderServiceId = null
            };
        }
    }
}