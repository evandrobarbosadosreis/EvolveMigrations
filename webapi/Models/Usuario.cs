using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{

    [Table("usuarios")]
    public class Usuario : Entidade
    {
        [Column("nome")]
        public string Nome { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}