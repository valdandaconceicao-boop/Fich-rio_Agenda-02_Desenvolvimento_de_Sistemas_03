using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras;

public partial class App : Application
{
    // Variável privada estática para guardar a instância única do nosso banco (Padrão Singleton)
    static SQLiteDatabaseHelper? _db;

    // Propriedade estática para permitir acessar o banco de qualquer tela do app via App.Db
    public static SQLiteDatabaseHelper Db
    {
        get
        {
            // Se o banco ainda não foi instanciado, vamos criar a conexão
            if (_db == null)
            {
                // Pega o caminho seguro no armazenamento local do dispositivo
                string pasta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!Directory.Exists(pasta))
                {
                    Directory.CreateDirectory(pasta);
                }

                string path = Path.Combine(pasta, "banco_sqlite_compras.db3");

                // Instancia o nosso helper com o caminho do arquivo .db3
                _db = new SQLiteDatabaseHelper(path);
            }
            return _db;
        }
    }

    public App()
    {
        InitializeComponent();
    }

    // Método que cria a janela inicial do aplicativo
    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Define a tela inicial com uma barra de navegação (NavigationPage) apontando para a Lista de Produtos
        return new Window(new NavigationPage(new Views.ListaProduto()));
    }
}