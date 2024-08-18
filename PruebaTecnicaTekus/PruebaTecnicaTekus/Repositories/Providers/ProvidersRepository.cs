using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

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
    }
}
