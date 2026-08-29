# MauiAppMinhasCompras - Agenda 04 (Desenvolvimento de Sistemas III)

Aplicativo desenvolvido em **.NET MAUI 9** com banco local **SQLite**. O foco desta atividade é a implementação de **busca dinâmica de produtos** utilizando `SearchBar`, evento `TextChanged` e `ObservableCollection<Produto>`.

## Requisitos atendidos

- [x] Pesquisa sobre .NET MAUI e mecanismos de busca
- [x] `SearchBar` na interface
- [x] Evento `TextChanged`
- [x] Uso de `TextChangedEventArgs` e `NewTextValue`
- [x] Uso de `ObservableCollection<Produto>`
- [x] Busca dinâmica pela descrição do produto
- [x] Atualização da lista durante a pesquisa
- [x] Persistência dos produtos em SQLite
- [x] Relatório com os desafios encontrados
- [x] Explicação sobre o uso de IA como apoio
- [x] Melhorias possíveis para a funcionalidade

## Implementação da busca

A `SearchBar` está definida em `Views/ListaProduto.xaml`:

```xml
<SearchBar
    x:Name="txt_busca"
    Placeholder="Buscar produto pela descrição..."
    TextChanged="txt_busca_TextChanged"
    SearchButtonPressed="txt_busca_SearchButtonPressed" />
```

A lista apresentada na tela utiliza uma `ObservableCollection<Produto>`:

```csharp
private ObservableCollection<Produto> lista_produtos_colecao = new();
private List<Produto> _todosOsProdutos = new();
```

A coleção é ligada à interface pelo `ItemsSource`:

```csharp
lista_produtos.ItemsSource = lista_produtos_colecao;
```

Quando o texto da busca muda, o evento utiliza `e.NewTextValue` e LINQ para filtrar os produtos pela descrição:

```csharp
private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
{
    if (_carregando) return;

    string termo = (e.NewTextValue ?? string.Empty).Trim();

    if (string.IsNullOrWhiteSpace(termo))
        AtualizarColecao(_todosOsProdutos);
    else
    {
        var filtrados = _todosOsProdutos.Where(p =>
            !string.IsNullOrEmpty(p.Descricao) &&
            p.Descricao.Contains(termo, StringComparison.OrdinalIgnoreCase));

        AtualizarColecao(filtrados);
    }
}
```

Os produtos são carregados do SQLite quando a tela aparece e ficam disponíveis em memória. Dessa forma, a busca pode atualizar a lista sem consultar novamente o banco a cada caractere digitado.

## Banco de dados

O arquivo `Helpers/SQLiteDatabaseHelper.cs` mantém as operações de cadastro, listagem, atualização e exclusão dos produtos. O projeto também possui um método auxiliar de pesquisa utilizando SQL `LIKE`.

## Pesquisa e apoio de IA

Durante a pesquisa e revisão da atividade foram utilizadas **Gemini/Antigravity** e **Qwen** como ferramentas de apoio. Elas ajudaram principalmente na compreensão do evento `TextChanged`, no uso da `ObservableCollection` e na revisão da lógica de filtragem.

Exemplo de pergunta utilizada durante a pesquisa:

> Como implementar busca instantânea com SearchBar no .NET MAUI usando ObservableCollection e LINQ?

## Relatório da Agenda 04

O relatório textual está disponível em [`docs/RELATORIO_AGENDA_04.md`](docs/RELATORIO_AGENDA_04.md).

O relatório responde às três questões solicitadas na atividade:

1. Quais desafios foram encontrados ao implementar a busca dinâmica?
2. Como a IA ajudou no processo de aprendizado e otimização do código?
3. Quais melhorias podem ser aplicadas na funcionalidade?

## Evidências do aplicativo Android

### Tela principal

![Tela principal com lista de produtos e SearchBar](docs/screenshots/01_lista_produtos.png)

Tela principal com a lista de produtos, campo de busca e total das compras.

### Novo produto

![Formulário de novo produto](docs/screenshots/02_novo_produto_form.png)

Tela utilizada para cadastrar um produto.

### Cadastro preenchido

![Cadastro de produto preenchido](docs/screenshots/03_cadastrar_produto.png)

Campos preenchidos para inclusão do produto.

### Edição de produto

![Tela de edição do produto](docs/screenshots/04_editar_produto.png)

Tela utilizada para alterar um produto cadastrado.

### Edição dos valores

![Edição do produto com teclado](docs/screenshots/05_editar_com_teclado.png)

Alteração dos dados durante a utilização do aplicativo.

### Confirmação

![Alerta de confirmação](docs/screenshots/06_alerta_sucesso.png)

Mensagem apresentada pelo aplicativo após a operação.

### Lista atualizada

![Lista atualizada com quatro produtos](docs/screenshots/07_lista_com_4_produtos.png)

Lista atualizada após o cadastro, mantendo a SearchBar disponível na tela.

## Estrutura principal

```text
Models/
└── Produto.cs

Helpers/
└── SQLiteDatabaseHelper.cs

Views/
├── ListaProduto.xaml
├── ListaProduto.xaml.cs
├── NovoProduto.xaml
├── NovoProduto.xaml.cs
├── EditarProduto.xaml
└── EditarProduto.xaml.cs

docs/
├── RELATORIO_AGENDA_04.md
└── screenshots/
    ├── 01_lista_produtos.png
    ├── 02_novo_produto_form.png
    ├── 03_cadastrar_produto.png
    ├── 04_editar_produto.png
    ├── 05_editar_com_teclado.png
    ├── 06_alerta_sucesso.png
    └── 07_lista_com_4_produtos.png
```

## Como executar

1. Abra `MauiAppMinhasCompras.sln` no Visual Studio 2022 com a carga de trabalho do .NET MAUI instalada.
2. Escolha um emulador Android ou dispositivo compatível.
3. Execute o projeto.

Também é possível compilar para Android pela linha de comando:

```bash
dotnet build -f net9.0-android
```

## Entrega

O repositório contém o código da implementação realizada na Agenda 04 e as evidências do aplicativo. O relatório da atividade é entregue em formato PDF, conforme solicitado.
