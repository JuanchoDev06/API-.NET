using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Reserva")]
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservaId { get; set; }

        [Required]
        public int ZonaComunId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required]
        [MaxLength(30)]
        public string Estado { get; set; }

        [ForeignKey("ZonaComunId")]
        public virtual ZonaComun? ZonaComun { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }
    }
}
