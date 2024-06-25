using System;

namespace Entidades
{
    public class Usuarios
    {
        public int Id { get; private set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }

        public Usuarios()
        {
            Id = 0;
            NombreUsuario = string.Empty;
            Contrasena = string.Empty;
        }

        public Usuarios(int id, string nombreUsuario, string contrasena)
        {
            Id = id;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
        }
    }
}
