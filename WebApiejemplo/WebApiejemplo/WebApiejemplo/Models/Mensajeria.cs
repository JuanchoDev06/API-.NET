using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Mensajeria")]
    public class Mensajeria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MensajeriaId { get; set; }

        [MaxLength(100)]
        public string? Empresa { get; set; }

        [MaxLength(100)]
        public string? Guia { get; set; }

        [Required]
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;

        public DateTime? FechaEntrega { get; set; }

        [Required]
        public int UnidadId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UnidadId")]
        public virtual Apartamentos? Unidad { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Vigilante { get; set; }
    }
}
