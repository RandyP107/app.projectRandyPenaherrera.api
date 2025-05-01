using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.DataAccess.context;
using app.projectRandyPenaherrera.Entities.Models;

namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public class VentaDetalleRepository : CrudGenericService<VentaDetalle>, IVentaDetalleRepository
    {
        public VentaDetalleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task ActualizarEntidad(VentaDetalle entity)
        {
            await UpdateEntity(entity);
        }

        public async Task EliminarEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<VentaDetalle> InsertarEntidad(VentaDetalle entity)
        {
            return await InsertEntity(entity);
        }

        public async Task<VentaDetalle> ObtenerEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<VentaDetalle>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }
    }
}
