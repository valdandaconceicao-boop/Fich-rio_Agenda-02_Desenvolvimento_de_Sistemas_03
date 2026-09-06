using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Code-behind da tela principal de listagem de produtos com ObservableCollection
    public partial class ListaProduto : ContentPage
    {
        // Coleção reativa observável vinculada diretamente à interface (CollectionView/ListView)
        private ObservableCollection<Produto> lista_produtos_colecao = new();

        // Lista em memória com todos os produtos carregados do banco SQLite
        private List<Produto> _todosOsProdutos = new();

        // Flag de segurança para bloquear a busca enquanto o banco carrega
        private bool _carregando = true;

        public ListaProduto()
        {
            InitializeComponent();
            
            // Vincula a ObservableCollection como fonte de dados (ItemsSource)
            lista_produtos.ItemsSource = lista_produtos_colecao;
        }

        // Executado toda vez que a página é exibida ou volta ao foco
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                _carregando = true;

                // Carrega todos os produtos do banco SQLite
                _todosOsProdutos = await App.Db.GetAll() ?? new List<Produto>();

                // Atualiza a ObservableCollection com os dados do banco
                AtualizarColecao(_todosOsProdutos);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar: {ex.Message}", "OK");
            }
            finally
            {
                _carregando = false;
            }
        }

        // Atualiza a coleção reativa e recalcula o valor total na barra inferior
        private void AtualizarColecao(IEnumerable<Produto> itens)
        {
            lista_produtos_colecao.Clear();
            foreach (var item in itens)
            {
                lista_produtos_colecao.Add(item);
            }

            // Recalcula o somatório dos itens visíveis
            double total = lista_produtos_colecao.Sum(p => p.Total);
            lbl_total_geral.Text = $"R$ {total:F2}";
        }

        // Disparado a cada caractere digitado ou apagado na SearchBar (Filtro Instantâneo)
        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_carregando) return;

            string termo = (e.NewTextValue ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(termo))
            {
                // Se a busca estiver vazia, exibe todos os produtos
                AtualizarColecao(_todosOsProdutos);
            }
            else
            {
                // Filtra em memória RAM instantaneamente usando LINQ
                var filtrados = _todosOsProdutos.Where(p =>
                    !string.IsNullOrEmpty(p.Descricao) &&
                    p.Descricao.Contains(termo, StringComparison.OrdinalIgnoreCase)
                );

                AtualizarColecao(filtrados);
            }
        }

        // Disparado ao pressionar Enter/Pesquisar no teclado virtual
        private void txt_busca_SearchButtonPressed(object sender, EventArgs e)
        {
            txt_busca_TextChanged(sender, new TextChangedEventArgs(
                txt_busca.Text ?? string.Empty,
                txt_busca.Text ?? string.Empty));
        }

        // Clique no botão "＋ Novo" para abrir a tela de cadastro
        private async void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        // Clique no botão "Somar" para exibir o popup com o total acumulado
        private async void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            try
            {
                double total = _todosOsProdutos.Sum(p => p.Total);
                await DisplayAlert("Total das Compras",
                    $"O valor total acumulado é: R$ {total:F2}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao calcular: {ex.Message}", "OK");
            }
        }

        // Toque no card para abrir a tela de edição
        private async void Frame_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Produto produtoSelecionado)
            {
                await Navigation.PushAsync(new EditarProduto(produtoSelecionado));
            }
        }

        // Flag de controle para evitar disparo duplo de ações simultâneas de swipe
        private bool _isProcessingSwipe = false;

        // Executa a navegação para edição de forma segura
        private async Task ExecutarEdicao(Produto? produto, SwipeView? swipeView = null)
        {
            if (_isProcessingSwipe || produto == null) return;
            _isProcessingSwipe = true;

            try
            {
                swipeView?.Close();
                await Navigation.PushAsync(new EditarProduto(produto));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao abrir edição: {ex.Message}", "OK");
            }
            finally
            {
                _isProcessingSwipe = false;
            }
        }

        // Executa o fluxo de exclusão com confirmação via DisplayAlert e proteção try-catch
        private async Task ExecutarExclusao(Produto? produto, SwipeView? swipeView = null)
        {
            if (_isProcessingSwipe || produto == null) return;
            _isProcessingSwipe = true;

            try
            {
                swipeView?.Close();

                // RECURSO NOVO 3: DisplayAlert de confirmação ("Tem certeza?")
                bool confirmar = await DisplayAlert(
                    "Confirmação de Exclusão",
                    $"Tem certeza que deseja excluir '{produto.Descricao}'?",
                    "Sim, excluir",
                    "Cancelar");

                if (!confirmar) return;

                // Exclui do banco SQLite de forma assíncrona
                await App.Db.Delete(produto.Id);

                // Remove da lista em memória e da ObservableCollection visível
                _todosOsProdutos.Remove(produto);
                lista_produtos_colecao.Remove(produto);

                // Recalcula o total na barra inferior
                double total = lista_produtos_colecao.Sum(p2 => p2.Total);
                lbl_total_geral.Text = $"R$ {total:F2}";

                await DisplayAlert("Sucesso!", "Produto excluído com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                // RECURSO NOVO 1: try-catch garante que se der erro o app não trave
                await DisplayAlert("Erro", $"Erro ao excluir: {ex.Message}", "OK");
            }
            finally
            {
                _isProcessingSwipe = false;
            }
        }

        // RECURSO DE AUTOMATIZAÇÃO: Disparado assim que o usuário faz o gesto de deslizar!
        // Deslizar para a esquerda -> abre a confirmação de exclusão automaticamente
        // Deslizar para a direita  -> abre a tela de edição automaticamente
        private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if (sender is SwipeView swipeView && swipeView.BindingContext is Produto produto)
            {
                // Gesto da Direita para a Esquerda (Left): Excluir
                if (e.SwipeDirection == SwipeDirection.Left)
                {
                    await ExecutarExclusao(produto, swipeView);
                }
                // Gesto da Esquerda para a Direita (Right): Editar
                else if (e.SwipeDirection == SwipeDirection.Right)
                {
                    await ExecutarEdicao(produto, swipeView);
                }
            }
        }

        // Handler do botão Editar (SwipeItemView ou SwipeItem)
        private async void SwipeItem_Editar_Invoked(object sender, EventArgs e)
        {
            Produto? produto = null;
            SwipeView? swipeView = null;

            if (sender is SwipeItemView swipeItemView)
            {
                produto = swipeItemView.CommandParameter as Produto;
                swipeView = swipeItemView.Parent?.Parent as SwipeView;
            }
            else if (sender is SwipeItem swipeItem)
            {
                produto = swipeItem.CommandParameter as Produto;
            }

            await ExecutarEdicao(produto, swipeView);
        }

        // Handler do botão Excluir (SwipeItemView ou SwipeItem)
        private async void SwipeItem_Excluir_Invoked(object sender, EventArgs e)
        {
            Produto? produto = null;
            SwipeView? swipeView = null;

            if (sender is SwipeItemView swipeItemView)
            {
                produto = swipeItemView.CommandParameter as Produto;
                swipeView = swipeItemView.Parent?.Parent as SwipeView;
            }
            else if (sender is SwipeItem swipeItem)
            {
                produto = swipeItem.CommandParameter as Produto;
            }

            await ExecutarExclusao(produto, swipeView);
        }
    }
}