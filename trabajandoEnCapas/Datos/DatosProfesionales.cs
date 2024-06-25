using System;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class DatosProfesionales : DatosConexionBD
    {
        public int tiendaLibros(string accion, Libros objtiendaLibros)
        {
            int resultado = -1;
            string orden = string.Empty;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conexion;

                if (accion == "Alta")
                {
                    orden = "INSERT INTO Libros (Titulo, Autor, Genero, Precio, Stock) VALUES (@Titulo, @Autor, @Genero, @Precio, @Stock)";
                }
                else if (accion == "Modificar")
                {
                    orden = "UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, Genero = @Genero, Precio = @Precio, Stock = @Stock WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", objtiendaLibros.id);
                }
                else if (accion == "Borrar")
                {
                    orden = "DELETE FROM Libros WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", objtiendaLibros.id);
                }

                cmd.CommandText = orden;

                cmd.Parameters.AddWithValue("@Titulo", objtiendaLibros.titulo);
                cmd.Parameters.AddWithValue("@Autor", objtiendaLibros.autor);
                cmd.Parameters.AddWithValue("@Genero", objtiendaLibros.genero);
                cmd.Parameters.AddWithValue("@Precio", objtiendaLibros.precio);
                cmd.Parameters.AddWithValue("@Stock", objtiendaLibros.stock);

                try
                {
                    abrirConexion();
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Error al tratar de guardar, borrar o modificar en tiendaLibros", e);
                }
                finally
                {
                    cerrarConexion();
                }
            }
            return resultado;
        }

        public DataSet listadoLibros(string cual)
        {
            string orden;
            if (cual != "Todos")
            {
                orden = "SELECT * FROM Libros WHERE ID_Libros = @ID_Libros";
            }
            else
            {
                orden = "SELECT * FROM Libros";
            }

            using (SqlCommand cmd = new SqlCommand(orden, conexion))
            {
                if (cual != "Todos")
                {
                    int idLibro;
                    if (int.TryParse(cual, out idLibro))
                    {
                        cmd.Parameters.AddWithValue("@ID_Libros", idLibro);
                    }
                    else
                    {
                        throw new ArgumentException("El parámetro 'cual' no es un número válido.");
                    }
                }

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    abrirConexion();
                    da.Fill(ds);
                }
                catch (Exception e)
                {
                    throw new Exception("Error al listar Libros", e);
                }
                finally
                {
                    cerrarConexion();
                }
                return ds;
            }
        }
    }
}
