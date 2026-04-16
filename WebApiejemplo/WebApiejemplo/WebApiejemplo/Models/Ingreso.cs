using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Ingreso")]
    public class Ingreso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngresoId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Tipo { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombrePersona { get; set; }

        [MaxLength(50)]
        public string? Documento { get; set; }

        [Required]
        public DateTime FechaHoraIngreso { get; set; } = DateTime.Now;

        public DateTime? FechaHoraSalida { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public int? UnidadId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Vigilante { get; set; }

        [ForeignKey("UnidadId")]
        public virtual Apartamentos? Unidad { get; set; }
    }
}