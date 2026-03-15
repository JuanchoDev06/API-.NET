using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("EMPRESA")]
    public class Empresa
    {
        [Key]
        public int IDEMPRESA { get; set; }  // Id para el registro
        public string? NOMBREEMPRESA { get; set; }
    }
}

