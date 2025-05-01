using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.DataAccess.context;
using app.projectRandyPenaherrera.Entities.Models;


namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public class CategoriaRepository : CrudGenericService<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Categoria> CreateCategoria(Categoria entity)
        {
            return await InsertEntity(entity);
        }

        public async Task DeleteCategoria(int id)
        {
            await DeleteEntity(id);
        }

        public async Task<Categoria> GetCategoria(int id)
        {
            return await SelectEntity(id);
        }

        public async Task<List<Categoria>> GetCategoriaLista()
        {
            return await SelectEntitiesAll();
        }

        public async Task UpdateCategoria(Categoria entity)
        {
            await UpdateEntity(entity);
        }
    }
}
