using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Libros
    {
        #region Atributos
        private int id;
        private int stock;
        private string titulo;
        private string autor;
        private string genero;
        private decimal precio;
        #endregion

        public Libros()
        {
            id = 0;
            stock = 0;
            precio = 0;
            titulo = string.Empty;
            autor = string.Empty;
            genero = string.Empty;
        }

        public int Id
        {
            get { return id; }
        }
 
        public int Stock
        {
            get { return stock; }
        }
        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }
        public string Genero
        {
            get { return genero; }
            set { genero = value; }
        }
    }
}
