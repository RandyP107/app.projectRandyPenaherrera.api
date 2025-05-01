using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Dto;

namespace app.projectRandyPenaherrera.services.Interfaces
{
    public interface IVentaService
    {
        Task<BaseResponse<VentaDto>> GetEntidad(int id);

        Task<BaseResponse<List<VentaDto>>> GetEntidadLista();

        Task<BaseResponse<VentaDto>> CrearEntidad(VentaDto request);

        Task<BaseResponse<VentaDto>> ActualizarEntidad(int id, VentaDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);
    }
}
