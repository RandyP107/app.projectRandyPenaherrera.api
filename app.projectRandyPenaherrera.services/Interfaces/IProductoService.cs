using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Dto;

namespace app.projectRandyPenaherrera.services.Interfaces
{
    public interface IProductoService

    {
        Task<BaseResponse<ProductoDto>> GetEntidad(int id);

        Task<BaseResponse<List<ProductoDto>>> GetEntidadLista();

        Task<BaseResponse<ProductoDto>> CrearEntidad(ProductoDto request);

        Task<BaseResponse<ProductoDto>> ActualizarEntidad(int id, ProductoDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);
    }
}
