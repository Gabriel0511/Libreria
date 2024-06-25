using System.Data.SqlClient;
using System.Data;
using Entidades;
using System;

namespace Datos
{
    public class DatosUsuarios : DatosConexionBD
    {
        public int RegistrarUsuario(Usuarios usuario)
        {
            string orden = "INSERT INTO Usuarios (NombreUsuario, Contrasena) VALUES (@NombreUsuario, @Contrasena)";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
            cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);

            try
            {
                conexion.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error al registrar usuario", e);
            }
            finally
            {
                conexion.Close();
                cmd.Dispose();
            }
        }

        public Usuarios ObtenerUsuario(string nombreUsuario, string contrasena)
        {
            string orden = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contrasena";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
            cmd.Parameters.AddWithValue("@Contrasena", contrasena);
            SqlDataReader reader = null;

            try
            {
                conexion.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Usuarios(
                        Convert.ToInt32(reader["Id"]),
                        reader["NombreUsuario"].ToString(),
                        reader["Contrasena"].ToString()
                    );
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener usuario", e);
            }
            finally
            {
                reader?.Close();
                conexion.Close();
                cmd.Dispose();
            }
        }

        public bool UsuarioExiste(string nombreUsuario)
        {
            string orden = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

            try
            {
                conexion.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception e)
            {
                throw new Exception("Error al verificar existencia de usuario", e);
            }
            finally
            {
                conexion.Close();
                cmd.Dispose();
            }
        }
    }
}
