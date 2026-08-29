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

## Parte 2 - Relatório técnico reflexivo

### Quais desafios foram encontrados ao implementar a busca dinâmica?

O principal desafio foi fazer a pesquisa responder rápido sem consultar o SQLite a cada caractere digitado. A solução utilizada foi carregar os produtos quando a tela aparece e fazer a filtragem sobre os dados já carregados. A `ObservableCollection` recebe os resultados e mantém a lista apresentada na interface atualizada.

### Como a IA ajudou no processo de aprendizado e otimização do código?

Ferramentas de inteligência artificial foram utilizadas como apoio durante o desenvolvimento. Elas ajudaram a entender melhor o evento `TextChanged`, revisar o uso da `ObservableCollection` e organizar a lógica de filtragem. A IA também foi usada para comparar alternativas e entender por que não era necessário consultar o banco novamente a cada letra digitada.

Exemplo de pergunta utilizada durante a pesquisa:

> Como implementar busca instantânea com SearchBar no .NET MAUI usando ObservableCollection e LINQ?

### Quais melhorias podem ser aplicadas na funcionalidade?

Uma melhoria futura seria adicionar um pequeno `debounce` antes de executar o filtro quando o usuário estiver digitando rapidamente. Também podem ser adicionados filtros por preço ou categoria e uma mensagem específica quando nenhum produto for encontrado.

## Evidências no código

- `Views/ListaProduto.xaml`: contém a `SearchBar`, `TextChanged` e a `CollectionView`.
- `Views/ListaProduto.xaml.cs`: contém a `ObservableCollection<Produto>`, o carregamento dos produtos e o filtro executado por `txt_busca_TextChanged`.
- `Helpers/SQLiteDatabaseHelper.cs`: contém as operações de persistência em SQLite e um método auxiliar de pesquisa com `LIKE`.
- `docs/screenshots/`: contém as capturas reais do aplicativo Android disponíveis no projeto.

## Observação sobre as capturas

As capturas atuais mostram a `SearchBar` presente no aplicativo, mas não registram um termo digitado com a lista já filtrada. Por esse motivo, elas não são apresentadas como prova de um teste visual que não foi capturado. A implementação da busca dinâmica pode ser conferida diretamente nos arquivos `Views/ListaProduto.xaml` e `Views/ListaProduto.xaml.cs`.

## Conclusão

Com a atividade foi possível aplicar uma busca dinâmica no .NET MAUI usando `SearchBar`, `TextChanged` e `ObservableCollection`. O banco SQLite continua responsável pelo armazenamento dos produtos, enquanto a filtragem da Agenda 04 acontece sobre os produtos já carregados em memória. Essa organização deixa a busca simples e evita consultas desnecessárias ao banco durante a digitação.
