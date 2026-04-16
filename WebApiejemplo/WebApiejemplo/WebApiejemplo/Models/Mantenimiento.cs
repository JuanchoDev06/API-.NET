using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Mantenimiento")]
    public class Mantenimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MantenimientoId { get; set; }

        [Required]
        public int TipoMantenimientoId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [MaxLength(150)]
        public string? Proveedor { get; set; }

        [MaxLength(300)]
        public string? Descripcion { get; set; }

        [MaxLength(30)]
        public string? Estado { get; set; } = "Pendiente";

        [Column(TypeName = "decimal(12, 2)")]
        public decimal? Costo { get; set; }

        public int? ZonaComunId { get; set; }

        [ForeignKey("TipoMantenimientoId")]
        public virtual TipoMantenimiento? TipoMantenimiento { get; set; }

        [ForeignKey("ZonaComunId")]
        public virtual ZonaComun? ZonaComun { get; set; }
    }
}
