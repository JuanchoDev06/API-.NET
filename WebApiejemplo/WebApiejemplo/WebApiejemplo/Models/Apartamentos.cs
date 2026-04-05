using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Apartamentos")]
    public class Apartamentos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnidadId { get; set; }

        public int? TorreId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Numero { get; set; }

        [Required]
        [MaxLength(30)]
        public string Tipo { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Area { get; set; }

        [ForeignKey("TorreId")]
        public virtual Torre? Torre { get; set; }
    }
}
