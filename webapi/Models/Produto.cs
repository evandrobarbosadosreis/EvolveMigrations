using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    [Table("produtos")]
    public class Produto : Entidade
    {
        [Column("nome")]
        public string Nome { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("preco_venda")]
        public decimal Venda { get; set; }

        [Column("preco_custo")]
        public decimal Custo { get; set; }
    }
}