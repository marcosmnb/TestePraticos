using Moq;
using SistemaBancario.Data.Repository;
using SistemaBancario.Exceptions;
using SistemaBancario.Model;
using SistemaBancario.Service;
using SistemaBancario.Service.Requester;

namespace SistemaBancario.Tests.ServiceHandlersTests
{
    public class GetSaldoServiceHandlerTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ReturnsSaldoResult()
        {
            // Arrange
            var mockRepository = new Mock<IContaCorrenteRepository>();

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ContaCorrente { IdContaCorrente = "9f99fd4d-53ee-495c-9bdd-fcaef5584a0b", Numero = 123, Nome = "Test", Ativo = true });

            mockRepository.Setup(repo => repo.GetSaldoAsync(It.IsAny<string>()))
                .ReturnsAsync(100);

            var query = new SaldoRequest { IdContaCorrente = "9f99fd4d-53ee-495c-9bdd-fcaef5584a0b" };
            var handler = new SaldoServiceHandler(mockRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123, result.Numero);
            Assert.Equal("Test", result.Nome);
            Assert.Equal(100, result.Saldo);
        }

        [Fact]
        public async Task Handle_InvalidAccount_ThrowsBusinessException()
        {
            // Arrange
            var mockRepository = new Mock<IContaCorrenteRepository>();

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ContaCorrente)null);

            var query = new SaldoRequest { IdContaCorrente = "1" };
            var handler = new SaldoServiceHandler(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
        }
    }

}
