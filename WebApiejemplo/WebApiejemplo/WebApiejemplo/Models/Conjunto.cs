using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Conjunto")]
    public class Conjunto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConjuntoId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(100)]
        public string? Ciudad { get; set; }

        [MaxLength(50)]
        public string? NIT { get; set; }

        [MaxLength(50)]
        public string? Telefono { get; set; }
    }
}
