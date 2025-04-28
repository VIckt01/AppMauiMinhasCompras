using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace AppMauiMinhasCompras
{
    public partial class App : Application
    {
        static AppDatabase database;

        public static AppDatabase Db
        {
            get
            {
                if (database == null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "produtos.db3");
                    database = new AppDatabase(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}

