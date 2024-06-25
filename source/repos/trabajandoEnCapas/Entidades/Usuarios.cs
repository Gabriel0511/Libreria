using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuarios
    {
        private int id;
        private string nombre;
        private string correo;
        private string direccion;
        private string carrito;

        public Usuarios()
        {
            id = 0;
            nombre = string.Empty;
            correo = string.Empty;
            direccion = string.Empty;
            carrito = string.Empty;
        }

        public int Id
        {
            get { return id; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Carrito
        {
            get { return carrito; }
            set { carrito = value; }
        }
    }
}
