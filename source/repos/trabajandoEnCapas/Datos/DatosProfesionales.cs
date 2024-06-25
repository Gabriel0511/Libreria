using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Entidades;

namespace Datos
{
    public class DatosProfesionales: DatosConexionBD
    {
        public int tiendaLibros(string accion, Libros objtiendaLibros)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
                orden = "insert into tiendaLibros values (" + objtiendaLibros.Titulo + ",'" + objtiendaLibros.Autor + ",'" + objtiendaLibros.Genero + ",'" + objtiendaLibros.Precio + ",'" + objtiendaLibros.Stock + ";";
            if (accion == "Modificar")
                orden = "update tiendaLibros set Titulo, Autor, Genero, Precio, Stock = '" + objtiendaLibros.Titulo + ",'" + objtiendaLibros.Autor + ",'" + objtiendaLibros.Genero + ",'" + objtiendaLibros.Precio + ",'" + objtiendaLibros.Stock + "'where id=" + objtiendaLibros.Id + ";";
            if (accion == "Borrar")
                orden = "delete from tiendaLibros where id=" + objtiendaLibros.Id + ";";

            OleDbCommand cmd = new OleDbCommand(orden, conexion);

            try
            {
                abrirConexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de guardar, borrar o modificar de tiendaLibros", e);
            }
            finally
            {
                cerrarConexion();
                cmd.Dispose();
            }
            return resultado;
        }
    }
}
