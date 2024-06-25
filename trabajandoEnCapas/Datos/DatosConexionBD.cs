using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class DatosConexionBD
    {
        public SqlConnection conexion;

        public DatosConexionBD()
        {
            conexion = new SqlConnection("server=DESKTOP-FIAAP59; database=tiendaLibros; integrated security=true");
        }

        public void abrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Broken || conexion.State == ConnectionState.Closed)
                    conexion.Open();
            }
            catch (SqlException e)
            {
                throw new Exception("Error al tratar de abrir la conexión a la base de datos", e);
            }
        }

        public void cerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            catch (SqlException e)
            {
                throw new Exception("Error al tratar de cerrar la conexión a la base de datos", e);
            }
        }

        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }
    }
}

