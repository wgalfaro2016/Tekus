using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Response.Providers;

namespace PruebaTecnicaTekus.Commands.Providers
{
    public class CreateProviderCommand : IRequest<ProviderResponse>
    {
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string NIT { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, ProviderResponse>
    {
        private readonly TekusContext _context;

        public CreateProviderCommandHandler(TekusContext context) {
            _context = context;
        }

        public async Task<ProviderResponse> Handle(CreateProviderCommand request, CancellationToken cancellationToken) {
            var provider = new Provider {
                Name = request.Name,
                LegalName = request.LegalName,
                NIT = request.NIT,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            _context.Providers.Add(provider);
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
