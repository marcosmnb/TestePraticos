using MediatR;
using SistemaBancario.Service.Response;

namespace SistemaBancario.Service.Requester
{
    public class SaldoRequest : IRequest<SaldoResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}
