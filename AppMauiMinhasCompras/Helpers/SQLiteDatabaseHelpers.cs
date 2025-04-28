using SQLite;
using AppMauiMinhasCompras.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMauiMinhasCompras
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto produto) => _database.InsertAsync(produto);

        public Task<int> Update(Produto produto) => _database.UpdateAsync(produto);

        public Task<int> Delete(Produto produto) => _database.DeleteAsync(produto);

        public Task<List<Produto>> GetAll() => _database.Table<Produto>().ToListAsync();

        public Task<List<Produto>> Search(string query) =>
            _database.Table<Produto>().Where(p => p.Descricao.Contains(query) || p.Categoria.Contains(query)).ToListAsync();
    }
}

