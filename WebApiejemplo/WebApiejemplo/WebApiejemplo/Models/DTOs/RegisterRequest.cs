using System.ComponentModel.DataAnnotations;

namespace WebApiejemplo.Models.DTOs
{
    public class RegisterRequest
    {
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
        public string Password { get; set; }

        [Required]
        public int RolId { get; set; }
    }
}
