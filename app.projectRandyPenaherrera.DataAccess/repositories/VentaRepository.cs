using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.DataAccess.context;
using app.projectRandyPenaherrera.Entities.Models;

namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public class VentaRepository : CrudGenericService<Venta>, IVentaRepository
    {
        public VentaRepository(AppDbContext context) : base(context)
        {

        }

        public async Task ActualizarEntidad(Venta entity)
        {
            await UpdateEntity(entity);
        }

        public async Task EliminarEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Venta> InsertarEntidad(Venta entity)
        {
            return await InsertEntity(entity);
        }

        public async Task<Venta> ObtenerEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Venta>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }
    }
}
