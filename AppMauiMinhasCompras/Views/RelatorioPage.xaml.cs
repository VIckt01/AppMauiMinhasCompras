using AppMauiMinhasCompras.Models;
using System;
using System.Collections.ObjectModel;

namespace AppMauiMinhasCompras.Views
{
    public partial class RelatorioPage : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public RelatorioPage()
        {
            InitializeComponent();
        }

        // Carregar os dados dos produtos
        protected async override void OnAppearing()
        {
            try
            {
                lista.Clear();
                var produtos = await App.Db.GetAll();
                produtos.ForEach(p => lista.Add(p));
                listView.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar o relatório: {ex.Message}", "OK");
            }
        }
    }
}


