using AppMauiMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System;

namespace AppMauiMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void SalvarProduto(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_descricao.Text))
                {
                    await DisplayAlert("Erro", "Descrição não pode ser vazia!", "OK");
                    return;
                }

                if (!double.TryParse(txt_quantidade.Text, out double quantidade))
                {
                    await DisplayAlert("Erro", "Quantidade inválida!", "OK");
                    return;
                }

                if (!double.TryParse(txt_preco.Text, out double preco))
                {
                    await DisplayAlert("Erro", "Preço inválido!", "OK");
                    return;
                }

                Produto produto = new Produto
                {
                    Descricao = txt_descricao.Text,
                    Quantidade = (int)quantidade,
                    Preco = preco,
                    Categoria = picker_categoria.SelectedItem.ToString()
                };

                await App.Db.Insert(produto);
                await DisplayAlert("Sucesso", "Produto adicionado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro: {ex.Message}", "OK");
            }
        }
    }
}

