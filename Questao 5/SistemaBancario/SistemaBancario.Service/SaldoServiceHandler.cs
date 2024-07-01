using MediatR;
using SistemaBancario.Data.Repository;
using SistemaBancario.Exceptions;
using SistemaBancario.Service.Requester;
using SistemaBancario.Service.Response;

namespace SistemaBancario.Service
{
    public class SaldoServiceHandler : IRequestHandler<SaldoRequest, SaldoResponse>
    {
        private readonly IContaCorrenteRepository _repository;

        public SaldoServiceHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaldoResponse> Handle(SaldoRequest request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync(request.IdContaCorrente)
                          ?? throw new BusinessException("Conta não encontrada.", "INVALID_ACCOUNT");

            if (!account.Ativo)
                throw new BusinessException("Conta inativa.", "INACTIVE_ACCOUNT");

            var saldo = await _repository.GetSaldoAsync(request.IdContaCorrente);

            return new SaldoResponse
            {
                Numero = account.Numero,
                Nome = account.Nome,
                DataHoraConsulta = DateTime.UtcNow,
                Saldo = saldo
            };
        }
    }
}
