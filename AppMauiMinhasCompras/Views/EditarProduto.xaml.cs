using AppMauiMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System;

namespace AppMauiMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (BindingContext is not Produto produto_anexado)
                {
                    await DisplayAlert("Erro", "Produto não encontrado!", "OK");
                    return;
                }

                // Converte a quantidade para int
                if (!double.TryParse(txt_quantidade.Text, out double quantidadeDouble))
                {
                    await DisplayAlert("Erro", "Quantidade inválida!", "OK");
                    return;
                }
                int quantidade = (int)quantidadeDouble;  // Faz a conversão explícita para int

                // Converte o preço para double
                if (!double.TryParse(txt_preco.Text, out double preco))
                {
                    await DisplayAlert("Erro", "Preço inválido!", "OK");
                    return;
                }

                Produto p = new Produto
                {
                    Id = produto_anexado.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = quantidade,  // Agora está usando int
                    Preco = preco,
                    Categoria = picker_categoria.SelectedItem.ToString()
                };

                await App.Db.Update(p);
                await DisplayAlert("Sucesso!", "Registro atualizado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", $"Erro: {ex.Message}", "OK");
            }
        }
    }
}