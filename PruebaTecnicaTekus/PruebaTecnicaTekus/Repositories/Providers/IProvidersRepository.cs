using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Repositories.Providers
{
    public interface IProvidersRepository
    {
        Task<List<ProvidersByCountryDto>> GetProvidersByCountry();
    }
}
