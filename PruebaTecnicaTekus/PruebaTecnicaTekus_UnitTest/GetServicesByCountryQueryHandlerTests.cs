using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Queries.Services;
using PruebaTecnicaTekus.Repositories.Services;

namespace PruebaTecnicaTekus_UnitTest
{
    public class GetServicesByCountryQueryHandlerTests
    {
        private readonly Mock<IServiceRepository> _serviceRepositoryMock;
        private readonly GetServicesByCountryQuery.GetServicesByCountryQueryHandler _handler;

        public GetServicesByCountryQueryHandlerTests() {
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _handler = new GetServicesByCountryQuery.GetServicesByCountryQueryHandler(_serviceRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfServicesByCountry_WhenDataExists() {
            // Arrange
            var servicesByCountry = new List<ServicesByCountryDto>
            {
            new ServicesByCountryDto { CountryName = "Country1", ServiceCount = 1 },
            new ServicesByCountryDto { CountryName = "Country2", ServiceCount = 2 }
        };

            _serviceRepositoryMock.Setup(repo => repo.GetServicesByCountryAsync())
                                  .ReturnsAsync(servicesByCountry);

            var query = new GetServicesByCountryQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Country1", result[0].CountryName);
            Assert.Equal(1, result[0].ServiceCount);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoDataExists() {
            // Arrange
            _serviceRepositoryMock.Setup(repo => repo.GetServicesByCountryAsync())
                                  .ReturnsAsync(new List<ServicesByCountryDto>());

            var query = new GetServicesByCountryQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
