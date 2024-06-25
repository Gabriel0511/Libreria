using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace Datos
{
    public class DatosConexionBD
    {
        public OleDbConnection conexion;
        public string cadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;DataSource=localhost;Persist Security Info=True";

        public DatosConexionBD()
        {
            conexion = new OleDbConnection(cadenaConexion);
        }
        public void abrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Broken || conexion.State == ConnectionState.Closed)
                    conexion.Open();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de abrir la conexión", e);
            }
        }

        public void cerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de cerrar la conexión", e);
            }
        }
    }
}
