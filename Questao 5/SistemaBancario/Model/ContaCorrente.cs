using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaBancario.Model
{
    public class ContaCorrente
    {
        [Key]
        public string IdContaCorrente { get; set; }

        [Required]
        [Column(TypeName = "INTEGER")]
        public int Numero { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "INTEGER")]
        public bool Ativo { get; set; }
    }
}
