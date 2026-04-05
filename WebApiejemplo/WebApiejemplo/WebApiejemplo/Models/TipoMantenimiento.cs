using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("TipoMantenimiento")]
    public class TipoMantenimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoMantenimientoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
}
