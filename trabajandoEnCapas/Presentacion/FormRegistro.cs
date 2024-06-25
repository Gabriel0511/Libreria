using System;
using System.Windows.Forms;
using Entidades;
using Negocios;

namespace Presentacion
{
    public partial class FormRegistro : Form
    {
        private NegUsuarios objNegUsuarios = new NegUsuarios();

        public FormRegistro()
        {
            InitializeComponent();
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtNombreUsuario.Text;
            string contrasena = txtContrasena.Text;

            if(!string.IsNullOrEmpty(nombreUsuario) && !string.IsNullOrEmpty(contrasena))
            {
                if(objNegUsuarios.UsuarioExiste(nombreUsuario))
                {
                    MessageBox.Show("El nombre de usuario ya existe. Por favor, elija otro");
                }
                else
                {
                    Usuarios nuevoUsuario = new Usuarios
                    {
                        NombreUsuario = nombreUsuario,
                        Contrasena = contrasena
                    };
                    try
                    {
                        objNegUsuarios.RegistrarUsuario(nuevoUsuario);
                        MessageBox.Show("Usuario registrado con éxito!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar usuario: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos.");
            }
        }
    }
}
