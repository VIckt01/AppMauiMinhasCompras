﻿using SQLite;

namespace AppMauiMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public string Categoria { get; set; }
    }
}
