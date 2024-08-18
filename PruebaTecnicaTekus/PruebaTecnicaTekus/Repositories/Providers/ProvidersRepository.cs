using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
using System.Threading;

namespace PruebaTecnicaTekus.Repositories.Providers
{
    public class ProvidersRepository: IProvidersRepository
    {
        private readonly TekusContext _context;

        public ProvidersRepository(TekusContext context) {
            _context = context;
        }

        public async Task<List<ProvidersByCountryDto>> GetProvidersByCountry() 
        {
            return await _context.ProvidersByCountryDtos
                    .FromSqlInterpolated($"EXEC GetProvidersByCountry")
                    .ToListAsync();
        }

        public async Task<int> AddAsync(Provider provider)
        {
            _context.Providers.Add(provider);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Provider provider) 
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Provider> GetByIdAsync(int id) 
        {
            return await _context.Providers.FindAsync(id);
        }

        public async Task<List<Provider>> GetProvidersListAsync()
        {
            return await _context.Providers.ToListAsync();
        }

        public async Task<Provider> GetProviderWithCustomFieldsAsync(int id) 
        {
            return await _context.Providers
               .Include(p => p.CustomProviderFields)
               .FirstOrDefaultAsync(p => p.ProviderID == id);
        }
    }
}
