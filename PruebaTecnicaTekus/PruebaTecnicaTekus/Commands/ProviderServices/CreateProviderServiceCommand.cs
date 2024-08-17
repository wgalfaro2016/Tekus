using AutoMapper;
using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
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
        private readonly TekusContext _context;
        private readonly IMapper _mapper;

        public CreateProviderServiceCommandHandler(TekusContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderServiceResponse> Handle(CreateProviderServiceCommand request, CancellationToken cancellationToken) {
            var providerService = new ProviderService {
                ProviderID = request.ProviderId,
                ServiceID = request.ServiceId,
                StartDate = request.StartDate
            };

            _context.ProviderServices.Add(providerService);
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
