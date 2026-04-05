using System.ComponentModel.DataAnnotations;

namespace WebApiejemplo.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Documento { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
