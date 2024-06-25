using System.Data;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegLibros
    {
        public DatosProfesionales _objDatosLibros = new DatosProfesionales();

        /// <summary>
        /// Realiza operaciones de alta, modificación o borrado en la base de datos de tiendaLibros.
        /// </summary>
        /// <param name="accion">La acción a realizar (Alta, Modificar, Borrar).</param>
        /// <param name="objTiendaLibros">El objeto Libros que contiene la información del libro.</param>
        /// <returns>El número de filas afectadas por la operación.</returns>
        public int TiendaLibros(string accion, Libros objTiendaLibros)
        {
            return _objDatosLibros.tiendaLibros(accion, objTiendaLibros);
        }

        /// <summary>
        /// Obtiene un listado de libros desde la base de datos.
        /// </summary>
        /// <param name="cual">El criterio de selección de los libros (puede ser "Todos" o un ID específico).</param>
        /// <returns>Un DataSet que contiene el listado de libros.</returns>
        public DataSet ListadoLibros(string cual)
        {
            return _objDatosLibros.listadoLibros(cual);
        }
    }
}

