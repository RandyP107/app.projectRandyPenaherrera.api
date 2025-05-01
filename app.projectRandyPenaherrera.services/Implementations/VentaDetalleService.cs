using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Dto;
using app.projectRandyPenaherrera.DataAccess.repositories;
using app.projectRandyPenaherrera.Entities.Models;
using app.projectRandyPenaherrera.services.EventMQ;
using app.projectRandyPenaherrera.services.Interfaces;

namespace app.projectRandyPenaherrera.services.Implementations
{
    public class VentaDetalleService : IVentaDetalleService
    {
        private readonly IVentaDetalleRepository _repository;
        private readonly IRabbitMQService _rabbitMQService;

        public VentaDetalleService(IVentaDetalleRepository repository, IRabbitMQService rabbitMQService)
        {
            _repository = repository;
            _rabbitMQService = rabbitMQService;
        }

        private VentaDetalleDto MapToVentaDetalleDto(VentaDetalle ventaDetalle)
        {
            return new VentaDetalleDto
            {
                Id = ventaDetalle.Id,
                VentaId = ventaDetalle.VentaId,
                NumeroItem = ventaDetalle.NumeroItem,
                ProductoId = ventaDetalle.ProductoId,
                PrecioUnitario = ventaDetalle.PrecioUnitario,
                Cantidad = ventaDetalle.Cantidad,
                Total = ventaDetalle.Total
            };
        }

        public async Task<BaseResponse<VentaDetalleDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<VentaDetalleDto>();
            try
            {
                

                var ventaDetalle = await _repository.ObtenerEntidad(id);
                if (ventaDetalle == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result =new VentaDetalleDto
                {
                    Id = ventaDetalle.Id,
                    VentaId = ventaDetalle.VentaId,
                    NumeroItem = ventaDetalle.NumeroItem,
                    ProductoId = ventaDetalle.ProductoId,
                    PrecioUnitario = ventaDetalle.PrecioUnitario,
                    Cantidad = ventaDetalle.Cantidad,
                    Total = ventaDetalle.Total
                };
            
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<List<VentaDetalleDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<VentaDetalleDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(MapToVentaDetalleDto).ToList();

                response.Success = response.Result.Count > 0;
                response.ErrorMessage = response.Success ? "Datos encontrados" : "Datos vacíos";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<VentaDetalleDto>> CrearEntidad(VentaDetalleDto request)
        {
            var response = new BaseResponse<VentaDetalleDto>();
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
                }

                VentaDetalle ventaDetalle = new()
                {
                    VentaId = request.VentaId,
                    NumeroItem = request.NumeroItem,
                    ProductoId = request.ProductoId,
                    PrecioUnitario = request.PrecioUnitario,
                    Cantidad = request.Cantidad,
                    Total = request.Total
                };

                ventaDetalle = await _repository.InsertarEntidad(ventaDetalle);

                response.Result = MapToVentaDetalleDto(ventaDetalle);
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(response.Result, "ventasDetalleQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<VentaDetalleDto>> ActualizarEntidad(int id, VentaDetalleDto request)
        {
            var response = new BaseResponse<VentaDetalleDto>();
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
                }

                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser un valor positivo.", nameof(id));
                }

                VentaDetalle ventaDetalle = new()
                {
                    Id = id,
                    VentaId = request.VentaId,
                    NumeroItem = request.NumeroItem,
                    ProductoId = request.ProductoId,
                    PrecioUnitario = request.PrecioUnitario,
                    Cantidad = request.Cantidad,
                    Total = request.Total
                };

                await _repository.ActualizarEntidad(ventaDetalle);

                response.Result = MapToVentaDetalleDto(ventaDetalle);
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = ventaDetalle.Id, Action = "Updated", VentaDetalle = response.Result }, "ventasDetalleQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<string>> EliminarEntidad(int id)
        {
            var response = new BaseResponse<string>();
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser un valor positivo.", nameof(id));
                }

                await _repository.EliminarEntidad(id);

                response.Result = "OK";
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = id, Action = "Deleted" }, "ventasDetalleQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}