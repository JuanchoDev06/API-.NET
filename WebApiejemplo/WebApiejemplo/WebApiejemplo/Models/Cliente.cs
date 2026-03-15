using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiejemplo.Models
{
    [Table("CLIENTE")]
    public class Cliente
    {
        public int ID { get; set; }  // Id para el registro
        public string? NIT { get; set; }
        public string? NOMBRE { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
