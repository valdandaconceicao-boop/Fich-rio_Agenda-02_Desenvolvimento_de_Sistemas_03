# Relatório - Agenda 04 - Desenvolvimento de Sistemas III

## Tema

Busca dinâmica de produtos no .NET MAUI usando `SearchBar`, `TextChanged` e `ObservableCollection<Produto>`.

## Parte 1 - Pesquisa e implementação

### O que é o .NET MAUI?

O .NET MAUI permite desenvolver aplicativos para diferentes plataformas usando uma base de código em C# e XAML. Neste projeto ele é usado para montar as telas do aplicativo Android e integrar a interface com os dados dos produtos.

### Como funciona uma busca dinâmica?

A busca dinâmica acompanha o texto digitado pelo usuário e atualiza os resultados enquanto o conteúdo da `SearchBar` muda. Dessa forma, não é necessário abrir outra tela ou confirmar a pesquisa a cada alteração.

### Como o evento TextChanged é usado?

A `SearchBar` da tela `Views/ListaProduto.xaml` está ligada ao método `txt_busca_TextChanged`. O método recebe `TextChangedEventArgs` e utiliza `e.NewTextValue` para obter o texto atual da pesquisa.

### Como a ObservableCollection é usada?

O arquivo `Views/ListaProduto.xaml.cs` possui uma `ObservableCollection<Produto>` chamada `lista_produtos_colecao`. Ela é ligada ao `ItemsSource` da `CollectionView`. Quando a busca muda, o programa atualiza essa coleção com os produtos encontrados.

### Como os produtos são filtrados?

Os produtos são carregados do SQLite quando a tela aparece e ficam disponíveis em memória. Durante a digitação, o código usa LINQ para comparar o termo pesquisado com `Produto.Descricao`, ignorando diferença entre letras maiúsculas e minúsculas. Se o campo de busca ficar vazio, todos os produtos voltam a ser exibidos.

### Exemplo aplicado ao projeto

Ao digitar parte do nome de um produto na `SearchBar`, o evento `TextChanged` recebe o texto atual por `e.NewTextValue`. Em seguida, o código filtra `_todosOsProdutos` e chama `AtualizarColecao`, deixando na `CollectionView` apenas os itens correspondentes.

## Parte 2 - Relatório técnico reflexivo

### Quais desafios foram encontrados ao implementar a busca dinâmica?

O principal desafio foi fazer a pesquisa responder rápido sem consultar o SQLite a cada caractere digitado. A solução utilizada foi carregar os produtos quando a tela aparece e fazer a filtragem sobre os dados já carregados. A `ObservableCollection` recebe os resultados e mantém a lista apresentada na interface atualizada.

### Como a IA ajudou no processo de aprendizado e otimização do código?

Durante a pesquisa e revisão da atividade foram utilizadas ferramentas de inteligência artificial como apoio, incluindo **Gemini/Antigravity** e **Qwen**. Elas ajudaram a entender melhor o evento `TextChanged`, revisar o uso da `ObservableCollection` e organizar a lógica de filtragem. A IA também foi usada para comparar alternativas e entender por que não era necessário consultar o banco novamente a cada letra digitada.

Exemplo de pergunta utilizada durante a pesquisa:

> Como implementar busca instantânea com SearchBar no .NET MAUI usando ObservableCollection e LINQ?

A IA foi utilizada como apoio ao estudo e à revisão. A implementação final foi conferida no código do próprio projeto.

### Quais melhorias podem ser aplicadas na funcionalidade?

Uma melhoria futura seria adicionar um pequeno `debounce` antes de executar o filtro quando o usuário estiver digitando rapidamente. Também podem ser adicionados filtros por preço ou categoria e uma mensagem específica quando nenhum produto for encontrado.

## Evidências reais do aplicativo

As capturas reais disponíveis no projeto ficam em `docs/screenshots/`. Cada arquivo é associado somente ao que realmente aparece na imagem:

| Arquivo | O que comprova |
|---|---|
| `01_lista_produtos.png` | Tela principal, SearchBar, produtos cadastrados e totalizador. |
| `02_novo_produto_form.png` | Formulário para inclusão de produto. |
| `03_cadastrar_produto.png` | Preenchimento dos dados de um novo produto. |
| `04_editar_produto.png` | Tela de edição de produto existente. |
| `05_editar_com_teclado.png` | Alteração de valores no formulário de edição. |
| `06_alerta_sucesso.png` | Confirmação apresentada pelo aplicativo após operação realizada. |
| `07_lista_com_4_produtos.png` | Lista atualizada com quatro produtos, SearchBar visível e novo total. |

Essas imagens são evidências complementares da execução real do aplicativo. Elas não são apresentadas como prova visual de uma pesquisa já filtrada, porque nenhuma das capturas disponíveis registra simultaneamente um termo digitado na `SearchBar` e a lista reduzida pelo filtro.

## Evidências da busca dinâmica no código

- `Views/ListaProduto.xaml`: contém a `SearchBar`, o evento `TextChanged`, `SearchButtonPressed` e a `CollectionView`.
- `Views/ListaProduto.xaml.cs`: contém `ObservableCollection<Produto>`, ligação ao `ItemsSource`, leitura de `e.NewTextValue`, filtro com LINQ e atualização da coleção.
- `Helpers/SQLiteDatabaseHelper.cs`: contém as operações de persistência em SQLite e um método auxiliar de pesquisa com `LIKE`.

A implementação da Agenda 04 pode ser auditada diretamente nesses arquivos. O filtro utilizado pelo evento `TextChanged` trabalha sobre os produtos já carregados em memória.

## Conclusão

Com a atividade foi possível aplicar uma busca dinâmica no .NET MAUI usando `SearchBar`, `TextChanged` e `ObservableCollection`. O banco SQLite continua responsável pelo armazenamento dos produtos, enquanto a filtragem da Agenda 04 acontece sobre os produtos já carregados em memória. Essa organização deixa a busca simples e evita consultas desnecessárias ao banco durante a digitação.
