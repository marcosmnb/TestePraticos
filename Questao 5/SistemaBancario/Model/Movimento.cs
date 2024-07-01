using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaBancario.Model
{
    public class Movimento
    {
        [Key]
        public string IdMovimento { get; set; }

        [Required]
        public string IdContaCorrente { get; set; }

        [Required]
        public string DataMovimento { get; set; }

        [Required]
        [MaxLength(1)]
        public string TipoMovimento { get; set; }

        [Required]
        [Column(TypeName = "REAL")]
        public decimal Valor { get; set; }

        [ForeignKey("IdContaCorrente")]
        public ContaCorrente ContaCorrente { get; set; }
    }
}
