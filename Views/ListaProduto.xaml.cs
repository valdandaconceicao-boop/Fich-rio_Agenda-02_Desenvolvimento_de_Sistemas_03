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
    }
}