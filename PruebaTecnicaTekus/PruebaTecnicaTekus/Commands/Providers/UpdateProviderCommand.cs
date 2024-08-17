using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Response.Providers;

namespace PruebaTecnicaTekus.Commands.Providers
{
    public class UpdateProviderCommand : IRequest<ProviderResponse>
    {
        public int ProviderID { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string NIT { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand,ProviderResponse>
    {
        private readonly TekusContext _context;

        public UpdateProviderCommandHandler(TekusContext context) {
            _context = context;
        }

        public async Task<ProviderResponse> Handle(UpdateProviderCommand request, CancellationToken cancellationToken) {
            var provider = await _context.Providers.FindAsync(request.ProviderID);
            if (provider == null) {
                return new ProviderResponse {
                    IsSuccess = false,
                    ProviderId = null
                };
            }

            provider.Name = request.Name;
            provider.LegalName = request.LegalName;
            provider.NIT = request.NIT;
            provider.Address = request.Address;
            provider.Phone = request.Phone;
            provider.Email = request.Email;

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) {
                return new ProviderResponse {
                    IsSuccess = true,
                    ProviderId = provider.ProviderID
                };
            }

            return new ProviderResponse {
                IsSuccess = false,
                ProviderId = null
            };
        }

      
    }
}
