# 📱 MauiAppMinhasCompras - Fichário Agenda 04 (.NET MAUI + SQLite)

> **Centro Estadual de Educação Tecnológica Paula Souza (CEETEPS / ETEC)**  
> **Curso:** Técnico em Desenvolvimento de Sistemas | **Componente:** Programação Mobile II (DS III)  
> **Aluno:** Valdan Conceição França  
> **Prazo Oficial de Entrega:** 31/08/2026 às 12:00  
> **Documento Oficial em PDF:** [`Fichario_Agenda04_DS3_ValdanConceicao.pdf`](Fichario_Agenda04_DS3_ValdanConceicao.pdf)

---

## 🧭 Readmap de Avaliação Pedagógica para o Professor / Tutor

Este readmap foi estruturado para facilitar a localização imediata de todos os itens exigidos nos enunciados oficiais:

### 📋 Agenda 04 — Pesquisa com IA e Relatório Reflexivo

| Requisito do Enunciado | Onde Identificar no Repositório | Onde Identificar no PDF | Status |
| :--- | :--- | :--- | :---: |
| **Parte 1: Pesquisa com IA (.NET MAUI, Busca, TextChanged, ObservableCollection)** | [`Views/ListaProduto.xaml.cs`](Views/ListaProduto.xaml.cs) e [`Views/ListaProduto.xaml`](Views/ListaProduto.xaml) | **Páginas 1 e 2 (Seção 2)** — Resolução detalhada das 5 questões conceituais. | ✅ **CONFORME** |
| **Parte 2: Relatório Reflexivo (Desafios, Uso de IA e Melhorias)** | Documentado neste README e no Code-Behind | **Página 2 (Seção 3)** — 3 respostas literais com citação de IA e prompt. | ✅ **CONFORME** |
| **Uso de `ObservableCollection<Produto>`** | [`Views/ListaProduto.xaml.cs` (Linhas 21 e 34)](Views/ListaProduto.xaml.cs#L21-L34) | **Páginas 3 e 5** — Código vetorial e print Full HD 1080p do IDE. | ✅ **CONFORME** |
| **Busca Dinâmica com `SearchBar` (Evento `TextChanged`)** | [`Views/ListaProduto.xaml` (Linha 13)](Views/ListaProduto.xaml) e [`ListaProduto.xaml.cs`](Views/ListaProduto.xaml.cs) | **Páginas 4 e 6** — XAML declarativo e print filtrando "arroz". | ✅ **CONFORME** |
| **Persistência Assíncrona SQLite (CRUD)** | [`Helpers/SQLiteDatabaseHelper.cs`](Helpers/SQLiteDatabaseHelper.cs) e [`Models/Produto.cs`](Models/Produto.cs) | **Páginas 3, 4 e 5** — Métodos `Insert`, `Update`, `Delete`, `GetAll`, `Search`. | ✅ **CONFORME** |
| **Competências 1 e Habilidades 1.1 a 1.5** | Projeto multiplataforma .NET MAUI 9 completo com banco e layout moderno | **Página 1 (Seção 1)** — Tabela de matriz de competências do CEETEPS. | ✅ **CONFORME** |

### 🆕 Agenda 05 — Recursos Novos e Design AAA Slim & Elegante

| # | Recurso Exigido / Melhoria | Onde Identificar no Código | Como Funciona | Status |
| :---: | :--- | :--- | :--- | :---: |
| 1️⃣ | **Try-catch** *(código que não trava quando dá erro)* | [`Views/ListaProduto.xaml.cs`](Views/ListaProduto.xaml.cs#L130-L200) | Todo o fluxo de exclusão e edição está envolvido em `try { } catch (Exception ex) { }`. Se ocorrer qualquer erro, o app **não fecha** — exibe um alerta nativo amigável. | ✅ **IMPLEMENTADO** |
| 2️⃣ | **Menu de contexto por deslize Duplo** *(SwipeView — Esquerda e Direita)* | [`Views/ListaProduto.xaml`](Views/ListaProduto.xaml#L43-L95) | **Multidirecional:**<br>• **Deslizar para a Direita (LeftItems):** Revela botão verde **"Editar"** que abre direto o formulário de edição.<br>• **Deslizar para a Esquerda (RightItems):** Revela botão vermelho **"Excluir"**. | ✅ **IMPLEMENTADO** |
| 3️⃣ | **DisplayAlert de confirmação** *(janelinha perguntando "Tem certeza?" antes de apagar)* | [`Views/ListaProduto.xaml.cs`](Views/ListaProduto.xaml.cs) | Após deslizar para a esquerda e tocar em "Excluir", abre o popup de confirmação: _"Tem certeza que deseja excluir '[produto]'?"_ com botões **"Sim, excluir"** e **"Cancelar"**. | ✅ **IMPLEMENTADO** |
| 💎 | **Design AAA Slim & Padronização Visual** *(Cards "meio quadrado com pontas circulares")* | [`Views/ListaProduto.xaml`](Views/ListaProduto.xaml), [`Views/NovoProduto.xaml`](Views/NovoProduto.xaml), [`Views/EditarProduto.xaml`](Views/EditarProduto.xaml) | **Padrão Visual Unificado:**<br>• Cantos arredondados consistentes (`RoundRectangle 16`) em todos os cards e formulários.<br>• Barra de busca integrada com cantos arredondados (`RoundRectangle 14`).<br>• Micro-badges elegantes para Quantidade e Preço Unitário.<br>• Barra flutuante de total com destaque em verde neon (`#34D399`).<br>• Contraste e tipografia calibrados para Dark Mode e Light Mode. | ✅ **PADRONIZADO** |

#### 🎬 Demonstração dos Recursos Novos e Design AAA (Agenda 05)

> **Prints e vídeo serão adicionados em breve** — demonstrando o fluxo completo: deslizar para a direita (editar), deslizar para a esquerda (excluir), confirmação por alerta e design slim no celular.

| Deslizar p/ Direita (Editar Verde) | Deslizar p/ Esquerda (Excluir Vermelho) | Janelinha "Tem certeza?" (DisplayAlert) |
| :---: | :---: | :---: |
| _print pendente_ | _print pendente_ | _print pendente_ |

---

## 📸 Demonstração do App em Execução no Celular (Fluxo Completo)

Abaixo estão as capturas de tela reais do aplicativo em execução em dispositivo móvel Android, organizadas na sequência lógica de operação:

### 1️⃣ Listagem Inicial e Busca em Tempo Real (`SearchBar` com "arroz")
| Lista Inicial (3 Itens - R$ 235,50) | 🔍 Busca em Tempo Real (Filtrando "arroz" - R$ 80,70) |
| :---: | :---: |
| ![Lista 3 Produtos](docs/screenshots/01_lista_produtos.png) | ![Busca Filtrando Arroz](docs/screenshots/08_busca_filtrando_arroz.png) |

### 2️⃣ Fluxo de Cadastro de Novo Produto
| Formulário Limpo | Preenchimento com Teclado | Confirmação de Sucesso (Alerta) |
| :---: | :---: | :---: |
| ![Formulário Limpo](docs/screenshots/02_novo_produto_form.png) | ![Preenchimento](docs/screenshots/03_cadastrar_produto.png) | ![Alerta Sucesso](docs/screenshots/06_alerta_sucesso.png) |

### 3️⃣ Fluxo de Edição, Exclusão e Lista Atualizada
| Tela de Edição / Exclusão | Ajuste com Teclado | Lista com 4 Itens (R$ 295,50) |
| :---: | :---: | :---: |
| ![Editar Produto](docs/screenshots/04_editar_produto.png) | ![Edição com Teclado](docs/screenshots/05_editar_com_teclado.png) | ![Lista 4 Produtos](docs/screenshots/07_lista_com_4_produtos.png) |

---

## 🎓 Relatório Técnico Reflexivo (Parte 2 do Enunciado)

### 💡 1. Desafios Encontrados na Busca Dinâmica
O principal desafio foi gerenciar a **concorrência e a fluidez visual (60 FPS)** durante a digitação acelerada do usuário. Realizar consultas repetidas ao banco de dados SQLite via `SELECT LIKE` a cada milissegundo de digitação causava concorrência de I/O e risco de *race conditions* (quando o retorno de uma busca anterior sobrescreve uma mais recente). A solução foi desacoplar o banco da busca: os dados são carregados uma única vez no `OnAppearing()` para a memória RAM, e a busca filtra diretamente a `ObservableCollection<Produto>` via LINQ em tempo constante.

### 🤖 2. Contribuição da IA e Declaração Ética de Transparência (Diretrizes CEETEPS)
Em total conformidade com as diretrizes do CEETEPS sobre integridade acadêmica, declara-se o uso de ferramentas de Inteligência Artificial Generativa (**Google Gemini / Antigravity** e **Qwen**) como assistentes de programação em par (*pair programming*).
- **Como a IA ajudou:** Auxiliou na estruturação da `ObservableCollection<Produto>` vinculada ao `ItemsSource`, na validação de queries parametrizadas com placeholders `?` contra SQL Injection e na formulação da lógica de recálculo dinâmico do somatório financeiro no evento `TextChanged`.
- **Prompt de Exemplo:** *"Como implementar a busca instantânea com SearchBar no .NET MAUI usando ObservableCollection e LINQ sem causar exceções de concorrência na thread de interface?"*

### 🚀 3. Melhorias Aplicáveis à Funcionalidade
1. **Padrão Debounce:** Adicionar atraso de 300ms antes de disparar o filtro para poupar ciclos de processamento enquanto o usuário digita continuamente.
2. **Filtros Multicritério:** Adicionar filtros simultâneos combinando busca por descrição com faixas de preço e categorias.
3. **Busca por Reconhecimento de Voz:** Integrar reconhecimento nativo de voz do Android diretamente no controle `SearchBar`.

---

## 🛠️ Estrutura do Projeto

```
📁 Fichário_Agenda 02_Desenvolvimento_de_Sistemas_03
├── 📁 Models/
│   └── Produto.cs                  # Entidade Produto (Mapeamento ORM SQLite)
├── 📁 Helpers/
│   └── SQLiteDatabaseHelper.cs     # Camada de Acesso a Dados (CRUD Assíncrono)
├── 📁 Views/
│   ├── ListaProduto.xaml (.cs)     # Tela Principal (ObservableCollection, Busca e Total)
│   ├── NovoProduto.xaml (.cs)      # Tela de Cadastro (Formulário e Validação)
│   └── EditarProduto.xaml (.cs)    # Tela de Edição e Exclusão
├── 📁 docs/
│   ├── 📁 screenshots/             # Evidências organizadas na sequência do app
│   └── 📁 evidencias_alta_resolucao/ # Prints originais em Full HD 1080p
├── 📁 APK_Instalador/
│   └── MinhasCompras_Android.apk   # Pacote compilado para instalação direta
├── Fichario_Agenda04_DS3_ValdanConceicao.pdf # Relatório Oficial da Agenda 04 em PDF
├── App.xaml (.cs)                  # Inicialização e Padrão Singleton (App.Db)
├── MauiProgram.cs                  # Bootstrapper e configuração de fontes/logs
├── MauiAppMinhasCompras.csproj     # Dependências (sqlite-net-pcl) e targets
└── .gitignore                      # Proteção contra pastas temporárias (bin/obj)
```

---

## 🚀 Como Executar o Projeto

### Opção 1: Linha de Comando (.NET CLI)
```bash
# Compilar para Windows
dotnet build -f net9.0-windows10.0.19041.0

# Compilar para Android
dotnet build -f net9.0-android
```

### Opção 2: Pelo Visual Studio 2022
1. Abra a solução `MauiAppMinhasCompras.sln`.
2. Selecione o dispositivo (Emulador Android ou Windows Machine).
3. Pressione `F5` para executar.

---

## 📦 Repositório GitHub

- **URL:** [https://github.com/valdandaconceicao-boop/Fich-rio_Agenda-02_Desenvolvimento_de_Sistemas_03](https://github.com/valdandaconceicao-boop/Fich-rio_Agenda-02_Desenvolvimento_de_Sistemas_03)
- **Branch:** `main`