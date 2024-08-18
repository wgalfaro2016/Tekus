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
    public class GetProviderServiceByIdQueryHandlerTests
    {
        private readonly Mock<IProviderServicesRepository> _providerServicesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetProviderServiceByIdQueryHandler _handler;

        public GetProviderServiceByIdQueryHandlerTests() {
            _providerServicesRepositoryMock = new Mock<IProviderServicesRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetProviderServiceByIdQueryHandler(_providerServicesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProviderServiceDto_WhenProviderServiceExists() {
            // Arrange
            var providerService = new ProviderService {
                ProviderServiceID = 1,
                ProviderID = 1,
                ServiceID = 1,
                StartDate = DateTime.Now
            };

            var providerServiceDto = new ProviderServiceDto {
                ProviderServiceId = 1,
                ProviderId = 1,
                ServiceId = 1,
                StartDate = DateTime.Now
            };

            _providerServicesRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(providerService);

            _mapperMock.Setup(mapper => mapper.Map<ProviderServiceDto>(providerService))
                .Returns(providerServiceDto);

            var query = new GetProviderServiceByIdQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(providerServiceDto.ProviderServiceId, result.ProviderServiceId);
            Assert.Equal(providerServiceDto.ProviderId, result.ProviderId);
            Assert.Equal(providerServiceDto.ServiceId, result.ServiceId);
            Assert.Equal(providerServiceDto.StartDate, result.StartDate);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenProviderServiceDoesNotExist() {
            // Arrange
            _providerServicesRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ProviderService)null);

            var query = new GetProviderServiceByIdQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
