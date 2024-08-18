using Moq;
using Xunit;
using PruebaTecnicaTekus.Commands.CustomProviderField;
using PruebaTecnicaTekus.Response.CustomProviderFieldResponse;
using PruebaTecnicaTekus.Models;
using System.Threading;
using System.Threading.Tasks;
using PruebaTecnicaTekus.Repositories.CustomProviderFields;

namespace PruebaTecnicaTekus_UnitTest
{
    public class CreateCustomFieldCommandHandlerTests
    {
        private readonly Mock<ICustomProviderFieldRepository> _repositoryMock;
        private readonly CreateCustomFieldCommandHandler _handler;

        public CreateCustomFieldCommandHandlerTests() {
            _repositoryMock = new Mock<ICustomProviderFieldRepository>();
            _handler = new CreateCustomFieldCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessTrue_WhenCustomFieldIsCreated() {
            // Arrange
            var command = new CreateCustomFieldCommand {
                ProviderId = 1,
                FieldName = "TestField",
                FieldValue = "TestValue",
                FieldType = "String"
            };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<CustomProviderField>()))
                           .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessFalse_WhenCustomFieldCreationFails() {
            // Arrange
            var command = new CreateCustomFieldCommand {
                ProviderId = 1,
                FieldName = "TestField",
                FieldValue = "TestValue",
                FieldType = "String"
            };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<CustomProviderField>()))
                           .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Id);
        }

    }
}