using SQLite;

namespace MauiAppMinhasCompras.Models
{
    // Classe modelo que representa a tabela Produto dentro do nosso banco SQLite
    public class Produto
    {
        // Chave primária que vai se auto-incrementar a cada novo registro (1, 2, 3...)
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Descrição ou nome do produto que vamos comprar (ex: Arroz, Feijão)
        public string Descricao { get; set; } = string.Empty;

        // Quantidade comprada do item
        public double Quantidade { get; set; }

        // Preço unitário do produto
        public double Preco { get; set; }

        // Propriedade calculada: multiplica a quantidade pelo preço para saber o total do item
        public double Total => Quantidade * Preco;
    }
}
