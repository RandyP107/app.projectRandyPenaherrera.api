using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using app.projectRandyPenaherrera.common.Dto;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Request;

namespace app.projectRandyPenaherrera.services.Interfaces
{
    public interface ICategoriaService
    {
        Task<BaseResponse<CategoriaDto>> GetCategoria(int id);

        Task<BaseResponse<List<CategoriaDto>>> GetCategoriaLista();

        Task<BaseResponse<CategoriaDto>> CrearCategoria(CategoriaRequest request);

        Task<BaseResponse<CategoriaDto>> ActualizarCategoria(int id, CategoriaRequest request);

        Task<BaseResponse<string>> EliminarCategoria(int id);


    }
}
