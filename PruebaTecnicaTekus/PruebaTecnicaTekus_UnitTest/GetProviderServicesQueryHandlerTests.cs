using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using PruebaTecnicaTekus.Dtos;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Queries.ProvidersServices;
using PruebaTecnicaTekus.Repositories.ProviderServices;
using Xunit;

namespace PruebaTecnicaTekus_UnitTest
{
    public class GetProviderServicesQueryHandlerTests
    {
        private readonly Mock<IProviderServicesRepository> _providerServicesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetProviderServicesQueryHandler _handler;

        public GetProviderServicesQueryHandlerTests() {
            _providerServicesRepositoryMock = new Mock<IProviderServicesRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetProviderServicesQueryHandler(_providerServicesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfProviderServiceDtos_WhenDataExists() {
            // Arrange
            var providerServices = new List<ProviderService>
            {
            new ProviderService { ProviderServiceID = 1, ProviderID = 1, ServiceID = 1, StartDate = DateTime.Now },
            new ProviderService { ProviderServiceID = 2, ProviderID = 2, ServiceID = 2, StartDate = DateTime.Now }
        };

            var providerServiceDtos = new List<ProviderServiceDto>
            {
            new ProviderServiceDto { ProviderServiceId = 1, ProviderId = 1, ServiceId = 1, StartDate = DateTime.Now },
            new ProviderServiceDto { ProviderServiceId = 2, ProviderId = 2, ServiceId = 2, StartDate = DateTime.Now }
        };

            _providerServicesRepositoryMock.Setup(repo => repo.GetProvidersServiceListAsync())
                .ReturnsAsync(providerServices);

            _mapperMock.Setup(mapper => mapper.Map<List<ProviderServiceDto>>(providerServices))
                .Returns(providerServiceDtos);

            var query = new GetProviderServicesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].ProviderServiceId);
            Assert.Equal(2, result[1].ProviderServiceId);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoDataExists() {
            // Arrange
            _providerServicesRepositoryMock.Setup(repo => repo.GetProvidersServiceListAsync())
                .ReturnsAsync(new List<ProviderService>());

            _mapperMock.Setup(mapper => mapper.Map<List<ProviderServiceDto>>(It.IsAny<List<ProviderService>>()))
                .Returns(new List<ProviderServiceDto>());

            var query = new GetProviderServicesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }


    }
}
