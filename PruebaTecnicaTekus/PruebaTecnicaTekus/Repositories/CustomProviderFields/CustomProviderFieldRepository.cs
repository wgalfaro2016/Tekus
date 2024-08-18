using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Repositories.CustomProviderFields
{
    public class CustomProviderFieldRepository : ICustomProviderFieldRepository
    {
        private readonly TekusContext _context;

        public CustomProviderFieldRepository(TekusContext context) {
            _context = context;
        }

        public async Task<int> AddAsync(CustomProviderField customProviderField) {
            _context.CustomProviderFields.Add(customProviderField);
            return await _context.SaveChangesAsync();
        }

        public async Task<CustomProviderField> GetByIdAsync(int id) {
            return await _context.CustomProviderFields.FindAsync(id);
        }
    }
}