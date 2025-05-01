using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.Entities.Models;

namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public interface IVentaDetalleRepository

    {
        Task<VentaDetalle> InsertarEntidad(VentaDetalle entity);

        Task EliminarEntidad(int id);

        Task<VentaDetalle> ObtenerEntidad(int id);

        Task<List<VentaDetalle>> ObtenerEntidadesLista();

        Task ActualizarEntidad(VentaDetalle entity);
    }
}
