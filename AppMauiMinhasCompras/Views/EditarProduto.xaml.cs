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
                    await DisplayAlert("Erro", "Produto n�o encontrado!", "OK");
                    return;
                }

                // Converte a quantidade para int
                if (!double.TryParse(txt_quantidade.Text, out double quantidadeDouble))
                {
                    await DisplayAlert("Erro", "Quantidade inv�lida!", "OK");
                    return;
                }
                int quantidade = (int)quantidadeDouble;  // Faz a convers�o expl�cita para int

                // Converte o pre�o para double
                if (!double.TryParse(txt_preco.Text, out double preco))
                {
                    await DisplayAlert("Erro", "Pre�o inv�lido!", "OK");
                    return;
                }

                Produto p = new Produto
                {
                    Id = produto_anexado.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = quantidade,  // Agora est� usando int
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