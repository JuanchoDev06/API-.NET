using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Parqueadero")]
    public class Parqueadero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParqueaderoId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tipo { get; set; }

        [Required]
        [MaxLength(20)]
        public string Numero { get; set; }

        public int? UnidadId { get; set; }

        [MaxLength(20)]
        public string? Placa { get; set; }

        [ForeignKey("UnidadId")]
        public virtual Apartamentos? Unidad { get; set; }
    }
}
