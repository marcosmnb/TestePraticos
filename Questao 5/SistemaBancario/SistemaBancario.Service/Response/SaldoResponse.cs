namespace SistemaBancario.Service.Response
{
    public class SaldoResponse
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public decimal Saldo { get; set; }
    }
}
