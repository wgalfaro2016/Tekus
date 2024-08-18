using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using PruebaTecnicaTekus.Commands.ProviderServices;
using PruebaTecnicaTekus.Repositories.ProviderServices;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Response.ProvidersService;
using AutoMapper;

namespace PruebaTecnicaTekus_UnitTest
{
    public class UpdateProviderServiceCommandHandlerTests
    {
        private readonly Mock<IProviderServicesRepository> _providerServicesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateProviderServiceCommandHandler _handler;

        public UpdateProviderServiceCommandHandlerTests() {
            _providerServicesRepositoryMock = new Mock<IProviderServicesRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateProviderServiceCommandHandler(_providerServicesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessTrue_WhenProviderServiceIsUpdated() {
            // Arrange
            var command = new UpdateProviderServiceCommand {
                ProviderServiceId = 1,
                ProviderId = 2,
                ServiceId = 3,
                StartDate = DateTime.UtcNow
            };

            var providerService = new ProviderService {
                ProviderServiceID = 1,
                ProviderID = 2,
                ServiceID = 3,
                StartDate = DateTime.UtcNow
            };

            _providerServicesRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProviderServiceId))
                                           .ReturnsAsync(providerService);

            _providerServicesRepositoryMock.Setup(repo => repo.UpdateAsync(providerService))
                                           .ReturnsAsync(1); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ProviderServiceId);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessFalse_WhenProviderServiceDoesNotExist() {
            // Arrange
            var command = new UpdateProviderServiceCommand {
                ProviderServiceId = 1,
                ProviderId = 2,
                ServiceId = 3,
                StartDate = DateTime.UtcNow
            };

            _providerServicesRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProviderServiceId))
                                           .ReturnsAsync((ProviderService)null); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.ProviderServiceId);
            Assert.Equal($"Provider service {command.ProviderServiceId} doesn't exists", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessFalse_WhenUpdateFails() {
            // Arrange
            var command = new UpdateProviderServiceCommand {
                ProviderServiceId = 1,
                ProviderId = 2,
                ServiceId = 3,
                StartDate = DateTime.UtcNow
            };

            var providerService = new ProviderService {
                ProviderServiceID = 1,
                ProviderID = 2,
                ServiceID = 3,
                StartDate = DateTime.UtcNow
            };

            _providerServicesRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProviderServiceId))
                                           .ReturnsAsync(providerService);

            _providerServicesRepositoryMock.Setup(repo => repo.UpdateAsync(providerService))
                                           .ReturnsAsync(0); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.ProviderServiceId);
            Assert.Equal($"There was a problem updating the provider service - {command.ProviderServiceId}", result.ErrorMessage);
        }

    }
}
