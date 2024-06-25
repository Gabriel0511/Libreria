using System;
using System.Data;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegUsuarios
    {
        private DatosUsuarios _objDatosUsuarios = new DatosUsuarios();

        public int RegistrarUsuario(Usuarios usuario)
        {
            return _objDatosUsuarios.RegistrarUsuario(usuario);
        }

        public Usuarios ObtenerUsuario(string nombreUsuario, string contrasena)
        {
            return _objDatosUsuarios.ObtenerUsuario(nombreUsuario, contrasena);
        }

        public bool UsuarioExiste(string nombreUsuario)
        {
            return _objDatosUsuarios.UsuarioExiste(nombreUsuario);
        }
    }
}

