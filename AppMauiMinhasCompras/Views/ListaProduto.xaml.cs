using System.Collections.ObjectModel;
using AppMauiMinhasCompras.Models;

namespace AppMauiMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        listView.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }


    private void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var produto = menuItem?.CommandParameter as Produto;

        if (produto != null)
        {
            lista.Remove(produto);

            // Remove o produto do banco de dados
            App.Db.DeleteProdutoAsync(produto);  // Aqui está a remoção no banco, se necessário

            // Exibe um alerta (opcional)
            DisplayAlert("Produto Removido", $"O produto {produto.Descricao} foi removido.", "OK");
        }
    }
}