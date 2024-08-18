using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Repositories.Services
{
    public class ServiceRepository: IServiceRepository
    {
        private readonly TekusContext _context;

        public ServiceRepository(TekusContext context) 
        {
            _context = context;
        }

        public async Task<List<ServicesByCountryDto>> GetServicesByCountryAsync()
        {
            return await _context.ServicesByCountryDtos
                    .FromSqlInterpolated($"EXEC GetServicesByCountry")
                    .ToListAsync();
        }
    }
}