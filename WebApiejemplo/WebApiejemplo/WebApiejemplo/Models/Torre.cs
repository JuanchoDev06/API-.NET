using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Torre")]
    public class Torre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TorreId { get; set; }

        [Required]
        public int ConjuntoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [ForeignKey("ConjuntoId")]
        public virtual Conjunto? Conjunto { get; set; }
    }
}
