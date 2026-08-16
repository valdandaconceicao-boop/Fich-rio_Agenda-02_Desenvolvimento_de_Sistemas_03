# 📱 MauiAppMinhasCompras - Agenda 02 (Desenvolvimento de Sistemas III)

Aplicativo multiplataforma desenvolvido em **.NET MAUI 9** com persistência local de dados em banco de dados **SQLite** assíncrono. O aplicativo gerencia uma lista de compras com controle de quantidade, preço unitário, cálculo automático de subtotais e somatória geral acumulada, além de operações CRUD completas (Criar, Listar, Atualizar e Excluir) e busca em tempo real.

---

## 📸 Demonstração do App em Execução no Celular

Abaixo estão as capturas de tela reais do aplicativo em execução em dispositivo móvel Android:

### 🛒 Listagem de Compras e Totalizador
| Lista com 3 Itens (Total R$ 235,50) | Lista com 4 Itens (Total R$ 295,50) |
| :---: | :---: |
| ![Lista 3 Produtos](docs/screenshots/01_lista_produtos.png) | ![Lista 4 Produtos](docs/screenshots/07_lista_com_4_produtos.png) |

### 📝 Fluxo de Cadastro de Novo Produto
| Formulário Limpo | Preenchimento com Teclado | Confirmação de Sucesso (Alerta) |
| :---: | :---: | :---: |
| ![Formulário Limpo](docs/screenshots/02_novo_produto_form.png) | ![Preenchimento](docs/screenshots/03_cadastrar_produto.png) | ![Alerta Sucesso](docs/screenshots/06_alerta_sucesso.png) |

### ✏️ Fluxo de Edição e Exclusão
| Tela de Edição / Exclusão | Ajuste de Valores com Teclado Numérico |
| :---: | :---: |
| ![Editar Produto](docs/screenshots/04_editar_produto.png) | ![Edição com Teclado](docs/screenshots/05_editar_com_teclado.png) |

---

## 🎓 Principais Aprendizados e Conceitos Aplicados

Durante o desenvolvimento deste projeto prático da **Agenda 02**, foram compreendidos e consolidados os seguintes tópicos fundamentais da engenharia de software móvel:

### 1. Multiplataforma com .NET MAUI e XAML
- Criação de interfaces declarativas em **XAML** desacopladas da lógica de negócios (**Code-Behind**).
- Navegação entre telas através do stack do MAUI (`NavigationPage`, `Navigation.PushAsync()` e `Navigation.PopAsync()`).
- Gerenciamento do ciclo de vida da tela com o evento `OnAppearing()`, garantindo atualização automática e re-renderização dos itens cadastrados ou editados ao retornar à tela principal.
- Adaptação dinâmica a temas de interface (**Dark Mode / Light Mode**) através de `AppThemeBinding`.

### 2. Banco de Dados Local com SQLite e ORM (`sqlite-net-pcl`)
- Mapeamento Objeto-Relacional (**ORM**), convertendo a classe `Produto` em uma tabela de banco relacional local usando as diretivas `[PrimaryKey, AutoIncrement]`.
- Criação e inicialização automática de tabelas na primeira execução do aplicativo.
- Armazenamento em diretório seguro e isolado da aplicação através de `Environment.SpecialFolder.LocalApplicationData`.

### 3. Programação Assíncrona (`async`, `await` e `Task`)
- Execução de todas as chamadas de banco de dados em segundo plano (background tasks), impedindo que operações de I/O travem ou congelem a thread de interface gráfica (**UI Thread**).

### 4. Padrão de Projeto Singleton
- Implementação de instância global e única da conexão com o banco em `App.Db`, evitando abertura concorrente de conexões desnecessárias e vazamentos de memória.

### 5. Boas Práticas de Segurança e Validação de Dados
- **Prevenção contra SQL Injection:** Uso de queries parametrizadas com placeholders (`?`) em instruções `UPDATE` e `SELECT LIKE`.
- **Tratamento Robusto de Tipos:** Validação de entradas numéricas com `double.TryParse` e `CultureInfo.InvariantCulture`, aceitando tanto vírgula quanto ponto decimal.
- **Diálogos de Confirmação:** Uso de `DisplayAlert` modal para confirmar exclusões antes de executar comandos destrutivos no banco de dados.

---

## 🐛 Correções Realizadas sobre o Material Didático

Durante o estudo e implementação do projeto, foram identificados e corrigidos 2 bugs presentes no material da aula:

1. **Correção no Método `Update` (Tipo de Retorno Inválido):**
   - *Problema do Material:* Indicava retorno `Task<List<Produto>>` para um comando `UPDATE`.
   - *Correção Aplicada:* Comandos SQL de atualização retornam o número de linhas afetadas. Foi ajustado para `Task<int>` utilizando `_conn.ExecuteAsync(sql, ...)`.

2. **Correção no Método `Search` (Sintaxe SQL Incompleta):**
   - *Problema do Material:* A query foi escrita sem a cláusula `FROM` (`SELECT * Produto WHERE...`), o que gerava erro de sintaxe no SQLite.
   - *Correção Aplicada:* Ajustada a sintaxe para `SELECT * FROM Produto WHERE Descricao LIKE ?`, utilizando parâmetros seguros para pesquisa.

---

## 🎯 Funcionalidades do Aplicativo

- [x] **Inserção de Produtos (`Insert`):** Cadastro de itens com descrição, quantidade e preço unitário com validações e feedback por alerta modal.
- [x] **Listagem Completa (`GetAll`):** Exibição dinâmica em cartões com nome, quantidade, preço unitário e subtotal formatado em moeda (`R$`).
- [x] **Totalizador Automático:** Barra inferior fixa com a somatória em tempo real de todas as compras cadastradas e botão **Somar** na barra superior.
- [x] **Busca em Tempo Real (`Search`):** Barra de pesquisa com filtro instantâneo por descrição via operador `LIKE`.
- [x] **Edição de Produtos (`Update`):** Alteração de qualquer campo com persistência imediata no SQLite via query parametrizada.
- [x] **Exclusão de Produtos (`Delete`):** Remoção de itens com diálogo de confirmação (`DisplayAlert`).
- [x] **Validação Segura de Entradas:** Tratamento de campos em branco e suporte a números com vírgula ou ponto decimal (`double.TryParse`).
- [x] **Interface Moderna e Responsiva:** Suporte a Tema Claro e Escuro (Dark Mode nativo).

---

## 🛠️ Estrutura do Código

```
📁 Fichário_Agenda 02_Desenvolvimento_de_Sistemas_03
├── 📁 Models/
│   └── Produto.cs                  # Entidade Produto (Mapeamento ORM SQLite)
├── 📁 Helpers/
│   └── SQLiteDatabaseHelper.cs     # Camada de Acesso a Dados (CRUD Assíncrono)
├── 📁 Views/
│   ├── ListaProduto.xaml (.cs)     # Tela Principal (Listagem, Busca e Total)
│   ├── NovoProduto.xaml (.cs)      # Tela de Cadastro (Formulário e Validação)
│   └── EditarProduto.xaml (.cs)    # Tela de Edição e Exclusão
├── 📁 docs/
│   └── 📁 screenshots/             # Evidências visuais do app em execução
├── 📁 APK_Instalador/
│   └── MinhasCompras_Android.apk   # Pacote compilado para instalação direta
├── App.xaml (.cs)                  # Inicialização e Padrão Singleton (App.Db)
├── MauiProgram.cs                  # Bootstrapper e configuração de fontes/logs
├── MauiAppMinhasCompras.csproj     # Dependências (sqlite-net-pcl) e targets
└── .gitignore                      # Proteção contra pastas temporárias (bin/obj)
```

---

## 🔍 Detalhamento dos Componentes

### 1. Modelo de Dados (`Models/Produto.cs`)
```csharp
using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total => Quantidade * Preco;
    }
}
```

### 2. Camada de Dados (`Helpers/SQLiteDatabaseHelper.cs`)
Implementa todas as operações CRUD de forma assíncrona com SQLite:
- `Insert(Produto p)` → `_conn.InsertAsync(p)`
- `Update(Produto p)` → `_conn.ExecuteAsync("UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?", ...)`
- `Delete(int id)` → `_conn.Table<Produto>().DeleteAsync(i => i.Id == id)`
- `GetAll()` → `_conn.Table<Produto>().ToListAsync()`
- `Search(string q)` → `_conn.QueryAsync<Produto>("SELECT * FROM Produto WHERE Descricao LIKE ?", "%" + q + "%")`

### 3. Padrão Singleton Global (`App.xaml.cs`)
```csharp
public static SQLiteDatabaseHelper Db
{
    get
    {
        if (_db == null)
        {
            string pasta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string path = Path.Combine(pasta, "banco_sqlite_compras.db3");
            _db = new SQLiteDatabaseHelper(path);
        }
        return _db;
    }
}
```

---

## 🚀 Como Executar o Projeto

### Opção 1: Pelo Visual Studio 2022
1. Abra o arquivo de solução `MauiAppMinhasCompras.sln`.
2. Certifique-se de que a carga de trabalho **Desenvolvimento do .NET MAUI** está instalada.
3. Selecione o dispositivo de destino: **Emulador Android**, **Windows Machine** ou **Celular Físico** (via Depuração USB).
4. Pressione `F5` para compilar e executar.

### Opção 2: Linha de Comando (.NET CLI)
```bash
# Compilar para Windows
dotnet build -f net9.0-windows10.0.19041.0

# Compilar para Android
dotnet build -f net9.0-android
```

### Opção 3: Instalação Direta no Celular (Arquivo APK)
O pacote compilado e pronto para uso está localizado em:
👉 **`APK_Instalador/MinhasCompras_Android.apk`**

---

## 📦 Repositório GitHub

- **URL:** [https://github.com/valdandaconceicao-boop/Fich-rio_Agenda-02_Desenvolvimento_de_Sistemas_03](https://github.com/valdandaconceicao-boop/Fich-rio_Agenda-02_Desenvolvimento_de_Sistemas_03)
- **Branch:** `main`
