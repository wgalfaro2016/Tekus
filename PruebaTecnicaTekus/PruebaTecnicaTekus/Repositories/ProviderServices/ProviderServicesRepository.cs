using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Repositories.ProviderServices
{
    public class ProviderServicesRepository: IProviderServicesRepository
    {
        private readonly TekusContext _context;

        public ProviderServicesRepository(TekusContext context) {
            _context = context;
        }
        public async Task<int> AddAsync(ProviderService provider) {
            _context.ProviderServices.Add(provider);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ProviderService provider) {
            return await _context.SaveChangesAsync();
        }

        public async Task<ProviderService> GetByIdAsync(int id) {
            return await _context.ProviderServices.FindAsync(id);
        }

        public async Task<List<ProviderService>> GetProvidersServiceListAsync() {
            return await _context.ProviderServices.ToListAsync();
        }
    }
}
