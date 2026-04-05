using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("ParqueaderoVisitante")]
    public class ParqueaderoVisitante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParqueaderoVisitanteId { get; set; }

        [Required]
        public int ParqueaderoId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Placa { get; set; }

        [Required]
        public DateTime FechaHoraIngreso { get; set; } = DateTime.Now;

        public DateTime? FechaHoraSalida { get; set; }

        [Required]
        public int IngresoId { get; set; }

        [ForeignKey("ParqueaderoId")]
        public virtual Parqueadero? Parqueadero { get; set; }

        [ForeignKey("IngresoId")]
        public virtual Ingreso? Ingreso { get; set; }
    }
}
