using System.Collections.ObjectModel;
using AppMauiMinhasCompras.Models;

namespace AppMauiMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Lista para armazenar todos os produtos
    private ObservableCollection<Produto> _todosProdutos;

    // Lista filtrada que será exibida
    public ObservableCollection<Produto> Produtos { get; set; }

    public ListaProduto()
    {
        InitializeComponent();

        // Inicializar a lista de produtos
        Produtos = new ObservableCollection<Produto>();
        BindingContext = this;

        // Carregar todos os produtos do banco de dados
        CarregarProdutos();
    }

    private async void CarregarProdutos()
    {
        // Carrega todos os produtos do banco de dados
        var listaProdutos = await App.Db.GetAll();
        _todosProdutos = new ObservableCollection<Produto>(listaProdutos);

        // Exibir todos os produtos inicialmente
        Produtos.Clear();
        foreach (var produto in _todosProdutos)
        {
            Produtos.Add(produto);
        }
    }

    // Evento que é disparado quando o texto do SearchBar é alterado
    private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        string textoBusca = e.NewTextValue?.ToLower() ?? "";  // Pega o texto inserido na SearchBar

        if (string.IsNullOrWhiteSpace(textoBusca))
        {
            // Se não houver texto, mostra todos os produtos
            Produtos.Clear();
            foreach (var produto in _todosProdutos)
            {
                Produtos.Add(produto);
            }
        }
        else
        {
            // Filtra os produtos com base no texto da busca
            var produtosFiltrados = await App.Db.Search(textoBusca);

            // Atualiza a lista de produtos exibida
            Produtos.Clear();
            foreach (var produto in produtosFiltrados)
            {
                Produtos.Add(produto);
            }
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NovoProduto());
    }
}

