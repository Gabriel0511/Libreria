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
        private int ID_Libros;
        private int Stock;
        private string Titulo;
        private string Autor;
        private string Genero;
        private decimal Precio;
        #endregion

        public Libros()
        {
            ID_Libros = 0;
            Stock = 0;
            Precio = 0;
            Titulo = string.Empty;
            Autor = string.Empty;
            Genero = string.Empty;
        }

        public int id
        {
            get { return ID_Libros; }
        }
 
        public int stock
        {
            get { return Stock; }
        }
        public decimal precio
        {
            get { return Precio; }
            set { Precio = value; }
        }
        public string titulo
        {
            get { return Titulo; }
            set { Titulo = value; }
        }
        public string autor
        {
            get { return Autor; }
            set { Autor = value; }
        }
        public string genero
        {
            get { return Genero; }
            set { Genero = value; }
        }
    }
}
