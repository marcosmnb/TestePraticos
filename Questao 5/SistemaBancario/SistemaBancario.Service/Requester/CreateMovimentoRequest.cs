using MediatR;

namespace SistemaBancario.Service.Requester
{
    public class CreateMovimentoRequest : IRequest<string>
    {
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public decimal Valor { get; set; }
        public string ChaveIdempotencia { get; set; }
    }
}
