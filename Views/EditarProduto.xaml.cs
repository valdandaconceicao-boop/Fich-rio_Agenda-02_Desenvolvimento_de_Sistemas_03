using System;
using System.Globalization;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Code-behind da tela de edição e exclusão de produto
    public partial class EditarProduto : ContentPage
    {
        // Guarda a referência do produto que está sendo editado
        private Produto? _produtoAtual;

        // Construtor padrão
        public EditarProduto()
        {
            InitializeComponent();
        }

        // Construtor que já recebe o produto selecionado na lista
        public EditarProduto(Produto produto)
        {
            InitializeComponent();
            _produtoAtual = produto;
            CarregarDados();
        }

        // Quando a página aparece, garante que os dados sejam carregados
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_produtoAtual == null && BindingContext is Produto p)
            {
                _produtoAtual = p;
                CarregarDados();
            }
        }

        // Preenche os campos da tela com as informações do produto
        private void CarregarDados()
        {
            if (_produtoAtual != null)
            {
                lbl_id.Text = _produtoAtual.Id.ToString();
                txt_descricao.Text = _produtoAtual.Descricao;
                txt_quantidade.Text = _produtoAtual.Quantidade.ToString(CultureInfo.InvariantCulture);
                txt_preco.Text = _produtoAtual.Preco.ToString("F2", CultureInfo.InvariantCulture);
            }
        }

        // Método que valida e atualiza os dados no banco SQLite
        private async void Atualizar()
        {
            try
            {
                if (_produtoAtual == null)
                {
                    await DisplayAlert("Erro", "Nenhum produto selecionado para edição.", "OK");
                    return;
                }

                // 1. Validação da Descrição
                if (string.IsNullOrWhiteSpace(txt_descricao.Text))
                {
                    await DisplayAlert("Atenção", "Por favor, preencha a descrição do produto.", "OK");
                    return;
                }

                // 2. Validação da Quantidade
                if (!double.TryParse(txt_quantidade.Text?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double quantidade) || quantidade <= 0)
                {
                    await DisplayAlert("Atenção", "Informe uma quantidade válida maior que zero.", "OK");
                    return;
                }

                // 3. Validação do Preço
                if (!double.TryParse(txt_preco.Text?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double preco) || preco < 0)
                {
                    await DisplayAlert("Atenção", "Informe um preço unitário válido.", "OK");
                    return;
                }

                // 4. Atualiza os dados no objeto
                _produtoAtual.Descricao = txt_descricao.Text.Trim();
                _produtoAtual.Quantidade = quantidade;
                _produtoAtual.Preco = preco;

                // 5. Executa a query de Update no banco
                await App.Db.Update(_produtoAtual);

                // 6. Alerta de sucesso e volta para a lista
                await DisplayAlert("Sucesso!", "Produto atualizado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao atualizar: {ex.Message}", "OK");
            }
        }

        // Método para excluir o produto do banco
        private async void Excluir()
        {
            try
            {
                if (_produtoAtual == null)
                {
                    await DisplayAlert("Erro", "Nenhum produto selecionado para exclusão.", "OK");
                    return;
                }

                // Pergunta para confirmar antes de deletar de verdade
                bool confirm = await DisplayAlert("Confirmação", $"Deseja realmente excluir o produto '{_produtoAtual.Descricao}'?", "Sim", "Não");

                if (confirm)
                {
                    // Chama o Delete do banco passando o ID
                    await App.Db.Delete(_produtoAtual.Id);
                    await DisplayAlert("Sucesso!", "Produto excluído com sucesso!", "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao excluir: {ex.Message}", "OK");
            }
        }

        // Clique no botão "Atualizar Produto"
        private void Button_Clicked_Atualizar(object sender, EventArgs e)
        {
            Atualizar();
        }

        // Clique no botão "Excluir Produto"
        private void Button_Clicked_Excluir(object sender, EventArgs e)
        {
            Excluir();
        }

        // Clique no botão de excluir da barra superior
        private void ToolbarItem_Clicked_Excluir(object sender, EventArgs e)
        {
            Excluir();
        }
    }
}
