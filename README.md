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
    {
        AtualizarColecao(_todosOsProdutos);
    }
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

## Evidências reais do aplicativo

As capturas reais do Android estão em `docs/screenshots/` e foram mantidas como evidências do que realmente foi executado.

| Evidência | O que aparece |
|---|---|
| `01_lista_produtos.png` | Tela principal, SearchBar, lista e total. |
| `02_novo_produto_form.png` | Formulário de novo produto. |
| `03_cadastrar_produto.png` | Cadastro preenchido. |
| `04_editar_produto.png` | Tela de edição. |
| `05_editar_com_teclado.png` | Alteração de valores. |
| `06_alerta_sucesso.png` | Confirmação apresentada pelo aplicativo. |
| `07_lista_com_4_produtos.png` | Lista atualizada, SearchBar e novo total. |

As imagens comprovam a execução do aplicativo e a presença da `SearchBar`. Nenhuma das capturas disponíveis registra ao mesmo tempo um termo digitado e a lista já reduzida. Por isso, a filtragem dinâmica é demonstrada pelo código real em `ListaProduto.xaml` e `ListaProduto.xaml.cs`, sem simular uma execução que não foi capturada.

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
APK_Instalador/
└── MinhasCompras_Android.apk
App.xaml
App.xaml.cs
MauiProgram.cs
MauiAppMinhasCompras.csproj
MauiAppMinhasCompras.sln
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
