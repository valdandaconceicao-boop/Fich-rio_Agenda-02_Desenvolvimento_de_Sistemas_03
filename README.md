# MauiAppMinhasCompras - Agenda 04 (Desenvolvimento de Sistemas III)

Aplicativo desenvolvido em **.NET MAUI 9** com banco local **SQLite**. Nesta Agenda 04, o foco é a implementação de **busca dinâmica de produtos** usando `SearchBar`, evento `TextChanged` e `ObservableCollection<Produto>`.

## O que foi pedido na Agenda 04

A atividade pede pesquisa e implementação de busca dinâmica em listas no .NET MAUI, utilizando uma `ObservableCollection` para armazenar os produtos e manter a interface atualizada.

No projeto, a implementação funciona assim:

1. Os produtos são carregados do SQLite quando a tela aparece.
2. `_todosOsProdutos` mantém os produtos carregados em memória.
3. `ObservableCollection<Produto>` é usada como fonte da lista exibida.
4. A `SearchBar` chama `txt_busca_TextChanged` sempre que o texto muda.
5. `e.NewTextValue` recebe o texto digitado.
6. LINQ filtra os produtos pela descrição.
7. A `ObservableCollection` é atualizada com os resultados.
8. Quando a pesquisa fica vazia, todos os produtos voltam a aparecer.

## Requisitos atendidos

- [x] Projeto em .NET MAUI.
- [x] SearchBar na interface.
- [x] Evento `TextChanged`.
- [x] Uso de `TextChangedEventArgs` e `NewTextValue`.
- [x] Uso de `ObservableCollection<Produto>`.
- [x] Busca dinâmica pela descrição do produto.
- [x] Atualização da lista durante a pesquisa.
- [x] Persistência em SQLite.
- [x] Relatório com desafios encontrados.
- [x] Explicação de como a IA ajudou.
- [x] Melhorias possíveis para a funcionalidade.

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

```xml
<CollectionView
    x:Name="lista_produtos"
    SelectionMode="None">
    ...
</CollectionView>
```

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

A busca usada pelo `TextChanged` é feita em memória depois que os produtos são carregados do SQLite. O helper também possui um método `Search` com SQL `LIKE`, mas ele fica disponível como método auxiliar e não é o mecanismo usado pelo evento atual.

## Banco SQLite

Arquivo: `Helpers/SQLiteDatabaseHelper.cs`

O projeto mantém as operações principais de banco de dados:

- `Insert(Produto p)` para cadastrar.
- `GetAll()` para listar.
- `Update(Produto p)` para atualizar.
- `Delete(int id)` para excluir.
- `Search(string q)` para pesquisa direta no SQLite usando `LIKE`.

As consultas SQL escritas manualmente utilizam parâmetros `?`.

## Relatório da atividade

### Desafios encontrados

O principal desafio foi fazer a pesquisa responder rápido sem consultar o SQLite a cada caractere digitado. Para resolver isso, os produtos são carregados quando a tela aparece e ficam em memória. A busca trabalha nessa lista e atualiza somente a coleção mostrada na tela.

### Como a IA ajudou

A inteligência artificial foi utilizada como apoio para entender melhor o evento `TextChanged`, revisar o uso da `ObservableCollection` e organizar a lógica do filtro. Também ajudou a comparar formas de evitar consultas desnecessárias ao banco durante a digitação.

Exemplo de pergunta utilizada:

> Como implementar busca instantânea com SearchBar no .NET MAUI usando ObservableCollection e LINQ?

### Melhorias possíveis

Uma melhoria futura seria aplicar um pequeno `debounce` antes de executar o filtro quando o usuário digitar muito rápido. Também podem ser adicionados filtros por preço ou categoria e uma mensagem quando nenhum produto for encontrado.

## Evidências do aplicativo

As capturas reais do aplicativo Android estão na pasta `docs/screenshots/`.

| Evidência | Arquivo |
|---|---|
| Lista inicial | `01_lista_produtos.png` |
| Formulário de cadastro | `02_novo_produto_form.png` |
| Cadastro preenchido | `03_cadastrar_produto.png` |
| Edição de produto | `04_editar_produto.png` |
| Edição com teclado | `05_editar_com_teclado.png` |
| Confirmação de cadastro | `06_alerta_sucesso.png` |
| Lista atualizada | `07_lista_com_4_produtos.png` |

As capturas atuais mostram a SearchBar presente no aplicativo, mas não registram um termo digitado com a lista já filtrada. Por isso, não apresento uma execução que não foi capturada. A implementação da busca dinâmica pode ser conferida diretamente em `Views/ListaProduto.xaml` e `Views/ListaProduto.xaml.cs`.

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

### Visual Studio 2022

1. Abra `MauiAppMinhasCompras.sln`.
2. Verifique se a carga de trabalho do .NET MAUI está instalada.
3. Escolha um emulador Android ou um dispositivo compatível.
4. Execute o projeto.

### Linha de comando

```bash
dotnet build -f net9.0-android
```

## Entrega

O repositório contém o código implementado durante a atividade. O relatório da Agenda 04 é entregue em formato PDF, conforme solicitado pelo professor.
