using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("ZonaComun")]
    public class ZonaComun
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZonaComunId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public bool RequierePago { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorHora { get; set; }
    }
}
