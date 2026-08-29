# MauiAppMinhasCompras - Agenda 04 (Desenvolvimento de Sistemas III)

Aplicativo desenvolvido em **.NET MAUI 9** com banco local **SQLite**. Nesta Agenda 04, o foco é a implementação de **busca dinâmica de produtos** usando `SearchBar`, evento `TextChanged` e `ObservableCollection<Produto>`.

## Requisitos da Agenda 04 atendidos

- [x] Pesquisa sobre .NET MAUI e mecanismos de busca.
- [x] `SearchBar` na interface.
- [x] Evento `TextChanged`.
- [x] Uso de `TextChangedEventArgs` e `NewTextValue`.
- [x] `ObservableCollection<Produto>` como coleção ligada à interface.
- [x] Busca dinâmica de produtos pela descrição.
- [x] Atualização da lista durante a digitação.
- [x] Persistência em SQLite.
- [x] Relatório com desafios encontrados.
- [x] Explicação de como a IA ajudou no aprendizado e revisão.
- [x] Melhorias possíveis para a funcionalidade.
- [x] Código disponível no repositório e relatório preparado em PDF para entrega.

## Como a busca funciona

1. Os produtos são carregados do SQLite quando a tela aparece.
2. `_todosOsProdutos` mantém os produtos em memória.
3. `ObservableCollection<Produto>` é ligada à `CollectionView` por `ItemsSource`.
4. A `SearchBar` chama `txt_busca_TextChanged` sempre que o texto muda.
5. `e.NewTextValue` recebe o texto digitado.
6. LINQ filtra os produtos pela descrição.
7. `AtualizarColecao` atualiza os itens exibidos.
8. Quando a pesquisa fica vazia, todos os produtos voltam a aparecer.

## SearchBar

Arquivo: `Views/ListaProduto.xaml`

```xml
<SearchBar
    x:Name="txt_busca"
    Placeholder="Buscar produto pela descrição..."
    TextChanged="txt_busca_TextChanged"
    SearchButtonPressed="txt_busca_SearchButtonPressed" />
```

A lista de produtos é apresentada em uma `CollectionView`.

## ObservableCollection e filtro em tempo real

Arquivo: `Views/ListaProduto.xaml.cs`

```csharp
private ObservableCollection<Produto> lista_produtos_colecao = new();
private List<Produto> _todosOsProdutos = new();
```

A coleção é ligada à interface:

```csharp
lista_produtos.ItemsSource = lista_produtos_colecao;
```

O filtro é executado quando o texto da SearchBar muda:

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

A busca usada pelo `TextChanged` acontece em memória depois que os produtos são carregados do SQLite. O helper também possui um método `Search` com SQL `LIKE`, disponível como método auxiliar.

## Banco SQLite

Arquivo: `Helpers/SQLiteDatabaseHelper.cs`

O projeto mantém as operações de cadastro, leitura, atualização e exclusão, além do método auxiliar de pesquisa. As consultas SQL escritas manualmente utilizam parâmetros `?`.

## Pesquisa e uso de IA

Durante a pesquisa e revisão foram utilizadas ferramentas de IA como apoio, incluindo **Gemini/Antigravity** e **Qwen**. Elas ajudaram na compreensão do `TextChanged`, no uso da `ObservableCollection` e na revisão da lógica do filtro.

Exemplo de pergunta utilizada:

> Como implementar busca instantânea com SearchBar no .NET MAUI usando ObservableCollection e LINQ?

A implementação final foi conferida no código do próprio projeto.

## Relatório da atividade

O relatório textual completo está em [`docs/RELATORIO_AGENDA_04.md`](docs/RELATORIO_AGENDA_04.md). A entrega acadêmica também possui versão em PDF.

Ele responde às três questões solicitadas:

1. Quais desafios foram encontrados ao implementar a busca dinâmica?
2. Como a IA ajudou no processo de aprendizado e otimização do código?
3. Quais melhorias podem ser aplicadas na funcionalidade?

# Evidências reais do aplicativo Android

Os prints abaixo são capturas reais do aplicativo em execução e estão armazenados em `docs/screenshots/`. Além de manter os arquivos no repositório, as imagens são exibidas diretamente neste README para facilitar a conferência pelo professor.

### 1. Tela principal - lista de produtos e SearchBar

![Tela principal com lista de produtos e SearchBar](docs/screenshots/01_lista_produtos.png)

Mostra a tela principal do aplicativo, a `SearchBar`, os produtos cadastrados e o total das compras.

### 2. Formulário de novo produto

![Formulário de novo produto](docs/screenshots/02_novo_produto_form.png)

Mostra a tela utilizada para cadastrar um novo produto.

### 3. Cadastro preenchido

![Cadastro de produto preenchido](docs/screenshots/03_cadastrar_produto.png)

Mostra os dados preenchidos antes da gravação do produto.

### 4. Tela de edição

![Tela de edição do produto](docs/screenshots/04_editar_produto.png)

Mostra um produto existente aberto para edição.

### 5. Edição com teclado

![Edição do produto com teclado](docs/screenshots/05_editar_com_teclado.png)

Mostra a alteração de dados do produto durante a utilização do aplicativo.

### 6. Confirmação apresentada pelo aplicativo

![Alerta de confirmação](docs/screenshots/06_alerta_sucesso.png)

Mostra o retorno visual apresentado após a operação realizada no aplicativo.

### 7. Lista atualizada

![Lista atualizada com quatro produtos](docs/screenshots/07_lista_com_4_produtos.png)

Mostra a lista depois do cadastro de um novo produto, com a `SearchBar` visível e o total atualizado.

## Relação das evidências com a Agenda 04

| Evidência | O que comprova |
|---|---|
| `01_lista_produtos.png` | Aplicativo executando, lista de produtos, SearchBar e total. |
| `02_novo_produto_form.png` | Tela de cadastro integrada ao projeto. |
| `03_cadastrar_produto.png` | Entrada de dados de produto. |
| `04_editar_produto.png` | Edição de produto existente. |
| `05_editar_com_teclado.png` | Interação real com os campos do aplicativo. |
| `06_alerta_sucesso.png` | Feedback visual do aplicativo após operação. |
| `07_lista_com_4_produtos.png` | Atualização da lista, SearchBar e total após cadastro. |

As capturas comprovam a execução real do aplicativo e mostram a `SearchBar` implementada na interface. As capturas disponíveis não registram simultaneamente um termo digitado e a lista já filtrada. A lógica da busca dinâmica pode ser conferida diretamente nos arquivos `Views/ListaProduto.xaml` e `Views/ListaProduto.xaml.cs`, onde estão `TextChanged`, `NewTextValue`, `ObservableCollection` e o filtro com LINQ.

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

O repositório contém o código implementado durante a atividade. O relatório da Agenda 04 é entregue em formato PDF, conforme solicitado pelo professor.
