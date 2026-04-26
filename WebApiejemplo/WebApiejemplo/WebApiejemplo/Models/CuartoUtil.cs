using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    public class CuartoUtil
    {
        [Key]
        public int CuartoUtilId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Numero { get; set; } = "";

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        // Relación opcional con Apartamento (Unidad)
        public int? UnidadId { get; set; }

        [ForeignKey("UnidadId")]
        public virtual Apartamentos? Unidad { get; set; }
    }
}
