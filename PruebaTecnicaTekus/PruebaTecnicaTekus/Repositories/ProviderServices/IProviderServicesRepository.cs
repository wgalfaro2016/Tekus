using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Repositories.ProviderServices
{
    public interface IProviderServicesRepository
    {
        Task<int> AddAsync(ProviderService provider);
        Task<int> UpdateAsync(ProviderService provider);
        Task<ProviderService> GetByIdAsync(int id);
        Task<List<ProviderService>> GetProvidersServiceListAsync();
    }
}