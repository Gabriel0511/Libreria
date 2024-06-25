using System;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class DatosCarritos : DatosConexionBD
    {
        public int CrearCarrito(int usuarioId)
        {
            string orden = "INSERT INTO Carritos (UsuarioId) OUTPUT INSERTED.CarritoId VALUES (@UsuarioId)";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

            try
            {
                abrirConexion();
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("Error al crear carrito", e);
            }
            finally
            {
                cerrarConexion();
                cmd.Dispose();
            }
        }

        public void AgregarLibroAlCarrito(int carritoId, int libroId, int cantidad)
        {
            string orden = "INSERT INTO CarritoLibros (CarritoId, LibroId, Cantidad) VALUES (@CarritoId, @LibroId, @Cantidad)";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@CarritoId", carritoId);
            cmd.Parameters.AddWithValue("@LibroId", libroId);
            cmd.Parameters.AddWithValue("@Cantidad", cantidad);

            try
            {
                abrirConexion();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al agregar libro al carrito", e);
            }
            finally
            {
                cerrarConexion();
                cmd.Dispose();
            }
        }

        public DataSet ObtenerCarrito(int carritoId)
        {
            string orden = @"SELECT L.Titulo, L.Autor, L.Genero, L.Precio, CL.Cantidad
                    FROM CarritoLibros CL
                    JOIN Libros L ON CL.LibroId = L.ID_Libros
                    WHERE CL.CarritoId = @CarritoId";

            DataSet ds = new DataSet();

            try
            {
                using (SqlCommand cmd = new SqlCommand(orden, conexion))
                {
                    cmd.Parameters.AddWithValue("@CarritoId", carritoId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    abrirConexion();
                    da.Fill(ds);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener carrito", e);
            }
            finally
            {
                cerrarConexion();
            }

            return ds;
        }


        public void EliminarLibroDelCarrito(int carritoId, int libroId)
        {
            string orden = "DELETE FROM CarritoLibros WHERE CarritoId = @CarritoId AND LibroId = @LibroId";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@CarritoId", carritoId);
            cmd.Parameters.AddWithValue("@LibroId", libroId);

            try
            {
                abrirConexion();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar del carrito", e);
            }
            finally
            {
                cerrarConexion();
                cmd.Dispose();
            }
        }

        public DataTable ObtenerLibrosDelCarrito(int usuarioId)
        {
            string orden = @"
                SELECT L.Titulo, L.Autor, L.Genero, L.Precio, CL.Cantidad
                FROM CarritoLibros CL
                JOIN Carritos C ON CL.CarritoId = C.CarritoId
                JOIN Libros L ON CL.LibroId = L.ID_Libros
                WHERE C.UsuarioId = @UsuarioId";

            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                abrirConexion();
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los libros del carrito", e);
            }
            finally
            {
                cerrarConexion();
                cmd.Dispose();
            }
            return dt;
        }

        public void EliminarTodosLosLibrosDelCarrito(int carritoId)
        {
            string orden = "DELETE FROM CarritoLibros";
            SqlCommand cmd = new SqlCommand(orden, conexion);

            try
            {
                abrirConexion();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar todos los libros del carrito", e);
            }
            finally
            {
                cerrarConexion();
                cmd.Dispose();
            }
        }

    }
}
