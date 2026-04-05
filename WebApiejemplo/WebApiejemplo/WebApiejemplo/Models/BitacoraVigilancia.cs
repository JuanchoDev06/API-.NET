using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("BitacoraVigilancia")]
    public class BitacoraVigilancia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BitacoraId { get; set; }

        [Required]
        public int VigilanteId { get; set; }

        [Required]
        public DateTime FechaHora { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string? Observacion { get; set; }

        [ForeignKey("VigilanteId")]
        public virtual Usuario? Vigilante { get; set; }
    }
}
