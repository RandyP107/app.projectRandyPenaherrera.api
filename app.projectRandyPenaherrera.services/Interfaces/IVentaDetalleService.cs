using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Dto;

namespace app.projectRandyPenaherrera.services.Interfaces
{
    public interface IVentaDetalleService

    {
            Task<BaseResponse<VentaDetalleDto>> GetEntidad(int id);

            Task<BaseResponse<List<VentaDetalleDto>>> GetEntidadLista();

            Task<BaseResponse<VentaDetalleDto>> CrearEntidad(VentaDetalleDto request);

            Task<BaseResponse<VentaDetalleDto>> ActualizarEntidad(int id, VentaDetalleDto request);

            Task<BaseResponse<string>> EliminarEntidad(int id);
        }
    }


