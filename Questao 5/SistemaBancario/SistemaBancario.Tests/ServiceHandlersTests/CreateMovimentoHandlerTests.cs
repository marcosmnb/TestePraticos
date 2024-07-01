using Moq;
using SistemaBancario.Data.Repository;
using SistemaBancario.Exceptions;
using SistemaBancario.Model;
using SistemaBancario.Service;
using SistemaBancario.Service.Requester;

namespace SistemaBancario.Tests.ServiceHandlersTests
{
    public class CreateMovimentoHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsId()
        {
            // Arrange
            var mockRepository = new Mock<IContaCorrenteRepository>();
            var mockIdempotenciaRepository = new Mock<IIdempotenciaRepository>();

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ContaCorrente { IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFB2B16C9", Numero = 123, Nome = "Test", Ativo = true });

            var command = new CreateMovimentoRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFB2B16C9",
                DataMovimento = "01/01/2023",
                TipoMovimento = "C",
                Valor = 100,
                ChaveIdempotencia = "chave-idempotencia"
            };

            var handler = new CreateMovimentoServiceHandler(mockRepository.Object, mockIdempotenciaRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(Guid.TryParse(result, out _));
        }

        [Fact]
        public async Task Handle_InvalidAccount_ThrowsBusinessException()
        {
            // Arrange
            var mockRepository = new Mock<IContaCorrenteRepository>();
            var mockIdempotenciaRepository = new Mock<IIdempotenciaRepository>();

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ContaCorrente)null);

            var command = new CreateMovimentoRequest
            {
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFB2B16C9",
                DataMovimento = "01/01/2023",
                TipoMovimento = "C",
                Valor = 100,
                ChaveIdempotencia = "chave-idempotencia"
            };

            var handler = new CreateMovimentoServiceHandler(mockRepository.Object, mockIdempotenciaRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
