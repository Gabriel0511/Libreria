using System;
using System.Data;
using Datos;
using Entidades;

namespace Negocios
{
    public class negCarritos
    {
        private DatosCarritos _objDatosCarritos = new DatosCarritos();

        public int CrearCarrito(int usuarioId)
        {
            return _objDatosCarritos.CrearCarrito(usuarioId);
        }

        public void AgregarLibroAlCarrito(int carritoId, int libroId, int cantidad)
        {
            _objDatosCarritos.AgregarLibroAlCarrito(carritoId, libroId, cantidad);
        }

        public DataSet ObtenerCarrito(int carritoId)
        {
            return _objDatosCarritos.ObtenerCarrito(carritoId);
        }

        public void EliminarLibroDelCarrito(int carritoId, int libroId)
        {
            _objDatosCarritos.EliminarLibroDelCarrito(carritoId, libroId);
        }

        public DataTable ObtenerLibrosDelCarrito(int usuarioId)
        {
            return _objDatosCarritos.ObtenerLibrosDelCarrito(usuarioId);
        }
        
        public void EliminarTodosLosLibrosDelCarrito(int carritoId)
        {
            _objDatosCarritos.EliminarTodosLosLibrosDelCarrito(carritoId);
        }
    }
}
