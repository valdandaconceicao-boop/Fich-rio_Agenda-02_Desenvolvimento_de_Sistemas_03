using System;
using System.Collections.Generic;
using System.Linq;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Code-behind da tela principal de listagem de produtos
    public partial class ListaProduto : ContentPage
    {
        // Lista em memória com todos os produtos carregados do banco
        private List<Produto> _todosOsProdutos = new();

        // Flag para bloquear a busca enquanto o banco ainda está carregando
        // Evita crash (race condition) quando o usuário digita antes do GetAll() terminar
        private bool _carregando = true;

        public ListaProduto()
        {
            InitializeComponent();
        }

        // Executado toda vez que a página é exibida ou volta ao foco
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // Marca como carregando: desativa a busca até terminar
                _carregando = true;

                // Carrega todos os produtos do banco SQLite
                _todosOsProdutos = await App.Db.GetAll() ?? new List<Produto>();

                // Exibe todos os produtos na lista e atualiza o total
                ExibirLista(_todosOsProdutos);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar: {ex.Message}", "OK");
            }
            finally
            {
                // Libera a busca: agora o usuário pode digitar na SearchBar
                _carregando = false;
            }
        }

        // Atribui uma lista ao ListView e atualiza o rodapé com o total
        private void ExibirLista(List<Produto> lista)
        {
            // Atribuir uma nova lista inteira ao ItemsSource é o método mais seguro no Android
            // Diferente de ObservableCollection.Clear()+Add() que causa crash no adaptador nativo
            lista_produtos.ItemsSource = lista;

            // Calcula e exibe o total geral no rodapé
            double total = 0;
            foreach (var p in lista)
            {
                total += p.Total;
            }
            lbl_total_geral.Text = $"R$ {total:F2}";
        }

        // Disparado a cada caractere digitado ou apagado na SearchBar
        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Se o banco ainda está carregando, ignora a digitação
            // Isso impede o crash quando o usuário digita antes do OnAppearing terminar
            if (_carregando) return;

            string termo = e.NewTextValue ?? string.Empty;

            if (string.IsNullOrWhiteSpace(termo))
            {
                // Sem filtro: exibe todos os produtos em memória
                ExibirLista(_todosOsProdutos);
            }
            else
            {
                // Filtra em memória (sem acessar o banco) para máxima velocidade e segurança
                string busca = termo.Trim();
                List<Produto> filtrados = new();

                foreach (var p in _todosOsProdutos)
                {
                    if (!string.IsNullOrEmpty(p.Descricao) &&
                        p.Descricao.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    {
                        filtrados.Add(p);
                    }
                }

                ExibirLista(filtrados);
            }
        }

        // Disparado ao pressionar enter/pesquisar no teclado virtual
        private void txt_busca_SearchButtonPressed(object sender, EventArgs e)
        {
            // Reutiliza a mesma lógica do TextChanged
            txt_busca_TextChanged(sender, new TextChangedEventArgs(
                txt_busca.Text ?? string.Empty,
                txt_busca.Text ?? string.Empty));
        }

        // Clique no botão "＋ Novo" para abrir a tela de cadastro
        private async void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        // Clique no botão "Somar" para exibir o popup com o total
        private async void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            try
            {
                double total = 0;
                foreach (var p in _todosOsProdutos)
                {
                    total += p.Total;
                }
                await DisplayAlert("Total das Compras",
                    $"O valor total acumulado é: R$ {total:F2}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao calcular: {ex.Message}", "OK");
            }
        }

        // Toque em um card da lista para abrir a tela de edição
        // Usamos o TapGestureRecognizer do card (ver XAML) porque a seleção
        // do CollectionView é instável no Android quando a lista é rebindada
        private async void Frame_Tapped(object sender, TappedEventArgs e)
        {
            // O CommandParameter do gesto carrega o próprio produto tocado
            if (e.Parameter is Produto produtoSelecionado)
            {
                // Abre a tela de edição passando o produto selecionado como parâmetro
                await Navigation.PushAsync(new EditarProduto(produtoSelecionado));
            }
        }
    }
}

