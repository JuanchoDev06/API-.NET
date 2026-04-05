using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("ResidenteUnidad")]
    public class ResidenteUnidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResidenteUnidadId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int UnidadId { get; set; }

        [Required]
        public bool EsPropietario { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Usuario { get; set; }

        [ForeignKey("UnidadId")]
        public virtual Apartamentos? Unidad { get; set; }
    }
}
