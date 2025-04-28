using AppMauiMinhasCompras.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace AppMauiMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();
            listView.ItemsSource = lista;
        }

        // Carregar produtos ao aparecer a página
        protected async override void OnAppearing()
        {
            try
            {
                lista.Clear();
                var produtos = await App.Db.GetAll();  // Certifique-se que App.Db é uma instância de AppDatabase
                foreach (var produto in produtos)
                {
                    lista.Add(produto);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar os produtos: {ex.Message}", "OK");
            }
        }

        // Método para abrir a página de novo produto
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        // Pesquisa de produtos
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string query = e.NewTextValue;
                lista.Clear();
                var produtos = await App.Db.Search(query);
                foreach (var produto in produtos)
                {
                    lista.Add(produto);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro na busca: {ex.Message}", "OK");
            }
        }

        // Método para editar o produto selecionado
        private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto p = e.SelectedItem as Produto;
                if (p != null)
                {
                    await Navigation.PushAsync(new EditarProduto { BindingContext = p });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao selecionar o produto: {ex.Message}", "OK");
            }
        }
    }
}
