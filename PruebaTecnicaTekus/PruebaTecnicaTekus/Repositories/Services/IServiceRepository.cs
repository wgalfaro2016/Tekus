using PruebaTecnicaTekus.Dtos;

namespace PruebaTecnicaTekus.Repositories.Services
{
    public interface IServiceRepository
    {
        Task<List<ServicesByCountryDto>> GetServicesByCountryAsync();
    }
}