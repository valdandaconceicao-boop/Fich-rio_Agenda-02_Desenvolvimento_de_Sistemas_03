using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers
{
    // Classe helper responsável por fazer a ponte entre o app e o banco SQLite
    public class SQLiteDatabaseHelper
    {
        // Conexão assíncrona com o banco para não travar a tela do usuário
        readonly SQLiteAsyncConnection _conn;

        // Construtor: recebe o caminho do arquivo .db e cria a tabela Produto se não existir
        public SQLiteDatabaseHelper(string path)
        {
            // Garante que o diretório pai exista antes de abrir a conexão
            string? dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            _conn = new SQLiteAsyncConnection(path);

            // Cria a tabela Produto de forma síncrona na inicialização
            // Usa GetAwaiter().GetResult() pois o construtor roda ANTES de qualquer tela abrir
            _conn.CreateTableAsync<Produto>().GetAwaiter().GetResult();
        }

        // Método para INSERIR um novo produto no banco
        // Retorna a quantidade de linhas afetadas (1 se inseriu com sucesso)
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // Método para ATUALIZAR um produto existente usando query SQL parametrizada
        // Retorna a quantidade de linhas afetadas
        public Task<int> Update(Produto p)
        {
            // Query SQL de UPDATE que altera a descrição, quantidade e preço onde o ID for igual
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            // ExecuteAsync é o correto para comandos UPDATE/DELETE/INSERT que não retornam linhas
            return _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        // Método para DELETAR um produto do banco pelo seu ID
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // Método para LISTAR TODOS os produtos cadastrados no banco
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        // Método para PESQUISAR produtos pela descrição usando o operador LIKE
        public Task<List<Produto>> Search(string q)
        {
            // Se a busca estiver vazia ou nula, retorna todos os produtos
            if (string.IsNullOrWhiteSpace(q))
            {
                return GetAll();
            }

            // Query com LIKE usando parâmetro "?" para evitar injeção SQL
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";

            // O parâmetro é montado com % antes e depois para buscar qualquer parte do texto
            string parametro = "%" + q.Trim() + "%";

            return _conn.QueryAsync<Produto>(sql, parametro);
        }
    }
}
