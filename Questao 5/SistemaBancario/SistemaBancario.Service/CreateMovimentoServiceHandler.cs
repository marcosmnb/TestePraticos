using MediatR;
using Newtonsoft.Json;
using SistemaBancario.Data.Repository;
using SistemaBancario.Exceptions;
using SistemaBancario.Model;
using SistemaBancario.Service.Requester;

namespace SistemaBancario.Service
{
    public class CreateMovimentoServiceHandler : IRequestHandler<CreateMovimentoRequest, string>
    {
        private readonly IContaCorrenteRepository _repository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public CreateMovimentoServiceHandler(IContaCorrenteRepository repository, IIdempotenciaRepository idempotenciaRepository)
        {
            _repository = repository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<string> Handle(CreateMovimentoRequest request, CancellationToken cancellationToken)
        {
            var idempotencyEntry = await _idempotenciaRepository.GetByChaveAsync(request.ChaveIdempotencia);
            if (idempotencyEntry != null)
            {
                return idempotencyEntry.Resultado;
            }
            var account = await _repository.GetByIdAsync(request.IdContaCorrente);

            ValidateMovimento(request, account);

            var movimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = request.IdContaCorrente,
                DataMovimento = request.DataMovimento,
                TipoMovimento = request.TipoMovimento,
                Valor = request.Valor
            };

            await _repository.AddMovimentoAsync(movimento);

            await _idempotenciaRepository.SaveAsync(new Idempotencia
            {
                ChaveIdempotencia = request.ChaveIdempotencia,
                Requisicao = JsonConvert.SerializeObject(request),
                Resultado = movimento.IdMovimento
            });

            return movimento.IdMovimento;
        }

        private void ValidateMovimento(CreateMovimentoRequest request, ContaCorrente account)
        {
            if (account == null)
            {
                throw new BusinessException("Conta não encontrada.", "INVALID_ACCOUNT");
            }
            if (!account.Ativo)
            {
                throw new BusinessException("Conta inativa.", "INACTIVE_ACCOUNT");
            }

            if (request.Valor <= 0)
            {
                throw new BusinessException("Valor inválido.", "INVALID_VALUE");
            }
            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
            {
                throw new BusinessException("Tipo de movimento inválido.", "INVALID_TYPE");
            }
        }
    }
}
