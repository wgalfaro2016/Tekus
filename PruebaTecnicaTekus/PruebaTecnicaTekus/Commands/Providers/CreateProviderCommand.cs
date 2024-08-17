using MediatR;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Commands.Providers
{
    public class CreateProviderCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string NIT { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, bool>
    {
        private readonly TekusContext _context;

        public CreateProviderCommandHandler(TekusContext context) {
            _context = context;
        }

        public async Task<bool> Handle(CreateProviderCommand request, CancellationToken cancellationToken) {
            var provider = new Provider {
                Name = request.Name,
                LegalName = request.LegalName,
                NIT = request.NIT,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            _context.Providers.Add(provider);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
