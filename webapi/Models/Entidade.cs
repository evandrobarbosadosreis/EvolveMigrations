using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public abstract class Entidade
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
    }
}