﻿using MediatR;
using PruebaTecnicaTekus.Data;

namespace PruebaTecnicaTekus.Commands.Providers
{
    public class UpdateProviderCommand : IRequest
    {
        public int ProviderID { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string NIT { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class UpdateProviderCommandHandler : IRequest<bool>
    {
        private readonly TekusContext _context;

        public UpdateProviderCommandHandler(TekusContext context) {
            _context = context;
        }

        public async Task<bool> Handle(UpdateProviderCommand request, CancellationToken cancellationToken) {
            var provider = await _context.Providers.FindAsync(request.ProviderID);
            if (provider == null) {
                return false;
            }

            provider.Name = request.Name;
            provider.LegalName = request.LegalName;
            provider.NIT = request.NIT;
            provider.Address = request.Address;
            provider.Phone = request.Phone;
            provider.Email = request.Email;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

      
    }
}
