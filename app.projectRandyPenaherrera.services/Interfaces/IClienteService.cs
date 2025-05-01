using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Dto;

namespace app.projectRandyPenaherrera.services.Interfaces
{
    public interface IClienteService
    {
        Task<BaseResponse<ClienteDto>> GetEntidad(int id);

        Task<BaseResponse<List<ClienteDto>>> GetEntidadLista();

        Task<BaseResponse<ClienteDto>> CrearEntidad(ClienteDto request);

        Task<BaseResponse<ClienteDto>> ActualizarEntidad(int id, ClienteDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);
    }
}
