using AppMauiMinhasCompras.Models;
using SQLite;

namespace AppMauiMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        public SQLiteDatabaseHelpers() { }

        readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelpers(string path)

        {  
            _connection = new SQLiteAsyncConnection(path);
         _connection.CreateTableAsync<Produto>().Wait();

        }
        public Task<int> insert(Produto p) 
        {
        
            return _connection.InsertAsync(p);
        }
        public Task<List<Produto>> update(Produto p) 
        {

            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE id=?";
          return _connection.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        
        }
        public Task<int> delete(int id)
         {
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == id);
           
        
        }
        public Task<List<Produto>> GetAll()
        
       {

           return _connection.Table<Produto>().ToListAsync();

        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'";

            return _connection.QueryAsync<Produto>(sql);
        
        }
    }
}
