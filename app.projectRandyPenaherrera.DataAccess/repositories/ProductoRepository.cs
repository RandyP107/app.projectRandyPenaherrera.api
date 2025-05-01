using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.DataAccess.context;
using app.projectRandyPenaherrera.Entities.Models;

namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public class ProductoRepository : CrudGenericService<Producto>, IProductoRepository
    {
        public ProductoRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Producto> InsertarEntidad(Producto entity)
        {
            return await InsertEntity(entity);
        }

        public async Task EliminarEntidad(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Producto> ObtenerEntidad(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Producto>> ObtenerEntidadesLista()
        {
            return await SelectEntitiesAll();
        }

        public async Task ActualizarEntidad(Producto entity)
        {
            await UpdateEntity(entity);
        }
    }
}
