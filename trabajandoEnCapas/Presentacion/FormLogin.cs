using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Negocios;

namespace Presentacion
{
    public partial class FormLogin : Form
    {
        private NegUsuarios objNegUsuarios = new NegUsuarios();
        public Usuarios usuarioActual { get; private set; }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtNombreUsuario.Text;
            string contrasena = txtContrasena.Text;

            Usuarios usuarioActual = objNegUsuarios.ObtenerUsuario(nombreUsuario, contrasena);

            if (usuarioActual != null)
            {
                this.Hide();
                Form1 form1 = new Form1(usuarioActual);
                form1.FormClosed += (s, args) => this.Close();
                form1.Show();
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            //Abrir el form de registro
            FormRegistro formRegistro = new FormRegistro();
            formRegistro.ShowDialog();
        }
    }
}
