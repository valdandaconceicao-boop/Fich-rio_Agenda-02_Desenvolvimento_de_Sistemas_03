using System;
using System.Globalization;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    // Code-behind da tela de cadastro de novo produto
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        // Método que valida os campos digitados e salva o produto no banco SQLite
        private async void Salvar()
        {
            try
            {
                // 1. Validação: A descrição não pode estar em branco
                if (string.IsNullOrWhiteSpace(txt_descricao.Text))
                {
                    await DisplayAlert("Atenção", "Por favor, preencha a descrição do produto.", "OK");
                    return;
                }

                // 2. Validação: A quantidade precisa ser um número válido e maior que zero
                if (!double.TryParse(txt_quantidade.Text?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double quantidade) || quantidade <= 0)
                {
                    await DisplayAlert("Atenção", "Informe uma quantidade válida maior que zero.", "OK");
                    return;
                }

                // 3. Validação: O preço precisa ser um número válido
                if (!double.TryParse(txt_preco.Text?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double preco) || preco < 0)
                {
                    await DisplayAlert("Atenção", "Informe um preço unitário válido.", "OK");
                    return;
                }

                // 4. Cria o objeto Produto com os dados preenchidos
                Produto p = new Produto
                {
                    Descricao = txt_descricao.Text.Trim(),
                    Quantidade = quantidade,
                    Preco = preco
                };

                // 5. Salva no banco de dados através da instância global App.Db
                await App.Db.Insert(p);

                // 6. Mensagem de sucesso para o usuário
                await DisplayAlert("Sucesso!", "Produto inserido com sucesso!", "OK");

                // 7. Volta para a tela anterior (Lista de Produtos)
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Se der algum erro inesperado, exibe o alerta
                await DisplayAlert("Erro", $"Ocorreu um erro ao salvar: {ex.Message}", "OK");
            }
        }

        // Evento de clique no botão "Salvar" da barra superior (Toolbar)
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Salvar();
        }

        // Evento de clique no botão "Salvar Produto" no corpo da página
        private void Button_Clicked_Salvar(object sender, EventArgs e)
        {
            Salvar();
        }
    }
}
