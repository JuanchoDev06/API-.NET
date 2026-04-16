using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        [Required]
        public int RolId { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Documento { get; set; }
        [MaxLength(150)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? Telefono { get; set; }
        [Required]
        public bool Activo { get; set; } = true;
        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; }
        [ForeignKey("RolId")]
        public virtual Rol? Rol { get; set; }

        public virtual ICollection<ResidenteUnidad>? ResidentesUnidad { get; set; }
    }
}