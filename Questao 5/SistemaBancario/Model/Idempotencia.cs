using System.ComponentModel.DataAnnotations;


namespace SistemaBancario.Model
{
    public class Idempotencia
    {
        [Key]
        public string ChaveIdempotencia { get; set; }
        public string Requisicao { get; set; }
        public string Resultado { get; set; }
    }
}
