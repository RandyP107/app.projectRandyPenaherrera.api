using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.Entities.Models;

namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetCategoria(int id);
        

        Task<Categoria> CreateCategoria(Categoria entity);

        Task<List<Categoria>> GetCategoriaLista();

        Task UpdateCategoria(Categoria entity);

        Task DeleteCategoria(int id);
    }
}
