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
    public partial class Form1 : Form
    {
        private NegLibros objNegLibros = new NegLibros();
        private NegUsuarios objNegUsuarios = new NegUsuarios();
        private negCarritos objNegCarritos = new negCarritos();
        private DataSet dsLibros;
        public Usuarios usuarioActual;
        private int carritoId;

        public Form1(Usuarios usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            carritoId = objNegCarritos.CrearCarrito(usuarioActual.Id);
            ConfigurarDGV();
            llenarDGV();

            //Cargar el carrito existente
            CargarCarrito();

            // Mostrar el formulario
            this.Show();

            // Guardar el estado del carrito al cerrar la aplicación
            this.FormClosed += Form1_FormClosed;
        }

        private void CargarCarrito()
        {
            DataTable dt = objNegCarritos.ObtenerLibrosDelCarrito(usuarioActual.Id);

            dgvCarrito.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvCarrito.Rows.Add(
                    row["Titulo"].ToString(),
                    row["Autor"].ToString(),
                    row["Genero"].ToString(),
                    Convert.ToDecimal(row["Precio"]).ToString("C2", new CultureInfo("en-US")),
                    row["Cantidad"].ToString()
                );
            }
            CalcularTotal();
        }

        private void ConfigurarDGV()
        {
            // Configurar las columnas del DataGridView principal (dgvLibros)
            dgvLibros.ColumnCount = 4;
            dgvLibros.Columns[0].Name = "Titulo";
            dgvLibros.Columns[0].HeaderText = "Titulo";
            dgvLibros.Columns[1].Name = "Autor";
            dgvLibros.Columns[1].HeaderText = "Autor";
            dgvLibros.Columns[2].Name = "Genero";
            dgvLibros.Columns[2].HeaderText = "Genero";
            dgvLibros.Columns[3].Name = "Precio";
            dgvLibros.Columns[3].HeaderText = "Precio";

            // Configurar las columnas del DataGridView del carrito (dgvCarrito)
            dgvCarrito.ColumnCount = 5;
            dgvCarrito.Columns[0].Name = "Titulo";
            dgvCarrito.Columns[0].HeaderText = "Titulo";
            dgvCarrito.Columns[1].Name = "Autor";
            dgvCarrito.Columns[1].HeaderText = "Autor";
            dgvCarrito.Columns[2].Name = "Genero";
            dgvCarrito.Columns[2].HeaderText = "Genero";
            dgvCarrito.Columns[3].Name = "Precio";
            dgvCarrito.Columns[3].HeaderText = "Precio";
            dgvCarrito.Columns[4].Name = "Cantidad";
            dgvCarrito.Columns[4].HeaderText = "Cantidad";
        }

        private void llenarDGV()
        {
            dsLibros = objNegLibros.ListadoLibros("Todos");

            if (dsLibros.Tables.Count > 0 && dsLibros.Tables[0].Rows.Count > 0)
            {
                MostrarDatosEnDGV(dsLibros);
                lblMensaje.Text = string.Empty; // Limpiar el mensaje de error si hay datos
            }
            else
            {
                dgvLibros.Rows.Clear();
                lblMensaje.Text = "No hay libros cargados en el sistema";
            }
        }

        private void MostrarDatosEnDGV(DataSet ds)
        {
            dgvLibros.Rows.Clear();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dgvLibros.Rows.Add(
                    dr["Titulo"].ToString(),
                    dr["Autor"].ToString(),
                    dr["Genero"].ToString(),
                    Convert.ToDecimal(dr["Precio"]).ToString("C2", new CultureInfo("en-US"))
                );
            }
        }

        private void buscador_TextChanged(object sender, EventArgs e)
        {
            FiltrarLibros(buscador.Text);
        }

        private void FiltrarLibros(string busqueda)
        {
            if (dsLibros != null && dsLibros.Tables.Count > 0)
            {
                try
                {
                    DataTable dt = dsLibros.Tables[0];

                    // Filtrar los datos usando LINQ
                    var librosFiltrados = dt.AsEnumerable().Where(row =>
                        row.Field<string>("Titulo").ToLower().Contains(busqueda.ToLower()) ||
                        row.Field<string>("Autor").ToLower().Contains(busqueda.ToLower()) ||
                        row.Field<string>("Genero").ToLower().Contains(busqueda.ToLower())
                    ).CopyToDataTable();

                    // Mostrar los datos filtrados en el DGV
                    MostrarDatosEnDGV(new DataSet { Tables = { librosFiltrados } });
                    lblMensaje.Text = string.Empty; // Limpiar el mensaje de error si hay resultados
                }
                catch (Exception)
                {
                    dgvLibros.Rows.Clear();
                    lblMensaje.Text = "No se encontraron libros que coincidan con la busqueda. :(";
                }
            }
            else
            {
                dgvLibros.Rows.Clear(); // Limpiar el dgv si no hay datos
                lblMensaje.Text = "No se encontraron libros que coincidan con la busqueda.:(";
            }
        }

        private void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count > 0 && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                DataGridViewRow row = dgvLibros.SelectedRows[0];
                string titulo = row.Cells["Titulo"].Value.ToString();
                string autor = row.Cells["Autor"].Value.ToString();
                string genero = row.Cells["Genero"].Value.ToString();
                decimal precio = decimal.Parse(row.Cells["Precio"].Value.ToString(), NumberStyles.Currency, new CultureInfo("en-US"));

                dgvCarrito.Rows.Add(titulo, autor, genero, precio.ToString("C2", new CultureInfo("en-US")), cantidad);

                // Sumando el total de precios
                CalcularTotal();

                // Agregar libro al carrito en la base de datos
                int libroId = ObtenerLibroId(titulo); // Implementa esta función para obtener el ID del libro según su título
                objNegCarritos.AgregarLibroAlCarrito(carritoId, libroId, cantidad);
            }
            else
            {
                lblMensaje.Text = "Seleccione un libro y asegúrese de que la cantidad sea válida.";
            }
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvCarrito.Rows)
            {
                if (row.Cells["Precio"].Value != null && row.Cells["Cantidad"].Value != null)
                {
                    decimal precio = decimal.Parse(row.Cells["Precio"].Value.ToString(), NumberStyles.Currency, new CultureInfo("en-US"));
                    int cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString());
                    total += precio * cantidad;
                }
            }
            lblTotal.Text = $"Total: {total.ToString("C2", new CultureInfo("en-US"))}";
        }

        private int ObtenerLibroId(string titulo)
        {
            // Verificar que dsLibros y su primera tabla no sean nulos
            if (dsLibros != null && dsLibros.Tables.Count > 0)
            {
                // Utilizar LINQ para buscar el título del libro y obtener su ID
                DataRow[] rows = dsLibros.Tables[0].Select($"Titulo = '{titulo}'");

                if (rows.Length > 0)
                {
                    return Convert.ToInt32(rows[0]["ID_Libros"]);
                }
                else
                {
                    throw new Exception("Libro no encontrado.");
                }
            }
            else
            {
                throw new Exception("DataSet de libros no inicializado o vacío.");
            }
        }


        private void btnEliminarDelCarrito_Click(object sender, EventArgs e)
        {
            if(dgvCarrito.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCarrito.SelectedRows[0];
                string titulo = row.Cells["Titulo"].Value.ToString();
                int libroId = ObtenerLibroId(titulo);

                //Eliminar el libro del carrito de la bd
                objNegCarritos.EliminarLibroDelCarrito(carritoId, libroId);

                //Eliminar el libro del dgv
                dgvCarrito.Rows.Remove(row);

                //Recalcular el total
                CalcularTotal();
            }
            else
            {
                lblMensaje.Text = "Seleccione un libro para eliminar del carrito";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Guardar el estado del carrito al cerrar la aplicación
            GuardarCarrito();
        }

        private void GuardarCarrito()
        {
            try
            {
                // Verificar que carritoId y datosCarritos estén inicializados
                if (carritoId != 0 && objNegCarritos != null)
                {
                    // Eliminar todos los libros del carrito en la base de datos
                    objNegCarritos.EliminarTodosLosLibrosDelCarrito(carritoId);

                    // Agregar los libros actuales del DataGridView al carrito en la base de datos
                    foreach (DataGridViewRow row in dgvCarrito.Rows)
                    {
                        if (row.Cells["Titulo"] != null && row.Cells["Titulo"].Value != null)
                        {
                            string titulo = row.Cells["Titulo"].Value.ToString();
                            string autor = row.Cells["Autor"].Value.ToString();
                            string genero = row.Cells["Genero"].Value.ToString();
                            decimal precio = decimal.Parse(row.Cells["Precio"].Value.ToString(), NumberStyles.Currency, new CultureInfo("en-US"));
                            int cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString());

                            int libroId = ObtenerLibroId(titulo);
                            objNegCarritos.AgregarLibroAlCarrito(carritoId, libroId, cantidad);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se puede guardar el carrito porque no está inicializado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el carrito: " + ex.Message);
            }
        }
    }
}
