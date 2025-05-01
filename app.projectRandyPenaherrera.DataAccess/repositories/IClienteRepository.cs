using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.Entities.Models;

namespace app.projectRandyPenaherrera.DataAccess.repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> InsertarEntidad(Cliente entity);

        Task EliminarEntidad(int id);

        Task<Cliente> ObtenerEntidad(int id);

        Task<List<Cliente>> ObtenerEntidadesLista();

        Task ActualizarEntidad(Cliente entity);
    }
}
