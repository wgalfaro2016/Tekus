using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;

namespace PruebaTecnicaTekus.Repositories.Providers
{
    public interface IProvidersRepository
    {
        Task<List<ProvidersByCountryDto>> GetProvidersByCountry();
        Task<int> AddAsync(Provider provider);
        Task<int> UpdateAsync(Provider provider);
        Task<List<Provider>> GetProvidersListAsync();
        Task<Provider> GetProviderWithCustomFieldsAsync(int id);
        Task<Provider> GetByIdAsync(int id);
    }
}
