using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using PruebaTecnicaTekus.Commands.Providers;
using PruebaTecnicaTekus.Repositories.Providers;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Response.Providers;

namespace PruebaTecnicaTekus_UnitTest
{
    public class CreateProviderCommandHandlerTests
    {
        private readonly Mock<IProvidersRepository> _providersRepositoryMock;
        private readonly CreateProviderCommandHandler _handler;

        public CreateProviderCommandHandlerTests() {
            _providersRepositoryMock = new Mock<IProvidersRepository>();
            _handler = new CreateProviderCommandHandler(_providersRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessTrue_WhenProviderIsCreated() {
            // Arrange
            var command = new CreateProviderCommand {
                Name = "Provider Name",
                LegalName = "Legal Name",
                NIT = "123456789",
                Address = "Provider Address",
                Phone = "1234567890",
                Email = "provider@example.com"
            };

            _providersRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Provider>()))
                                    .ReturnsAsync(1); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ProviderId);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessFalse_WhenProviderCreationFails() {
            // Arrange
            var command = new CreateProviderCommand {
                Name = "Provider Name",
                LegalName = "Legal Name",
                NIT = "123456789",
                Address = "Provider Address",
                Phone = "1234567890",
                Email = "provider@example.com"
            };

            _providersRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Provider>()))
                                    .ReturnsAsync(0); 

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.ProviderId);
            Assert.Equal("There was a problem inserting a provider", result.ErrorMessage);
        }
    }
}
