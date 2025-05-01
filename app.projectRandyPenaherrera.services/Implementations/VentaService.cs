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
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _repository;
        private readonly IRabbitMQService _rabbitMQService;

        public VentaService(IVentaRepository repository, IRabbitMQService rabbitMQService)
        {
            _repository = repository;
            _rabbitMQService = rabbitMQService;
        }

        private VentaDto MapToVentaDto(Venta venta)
        {
            return new VentaDto
            {
                Id = venta.Id,
                ClienteId = venta.ClienteId,
                FechaVenta = venta.FechaVenta,
                NumeroFactura = venta.NumeroFactura,
                MetodoPago = venta.MetodoPago
            };
        }

        public async Task<BaseResponse<VentaDto>> ActualizarEntidad(int id, VentaDto request)
        {
            var response = new BaseResponse<VentaDto>();
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

                Venta venta = new()
                {
                    Id = id,
                    ClienteId = request.ClienteId,
                    FechaVenta = DateTime.Now,
                    NumeroFactura = request.NumeroFactura,
                    MetodoPago = request.MetodoPago,
                    Fecha = DateTime.Now,
                    Estado = true
                };

                await _repository.ActualizarEntidad(venta);

                response.Result = MapToVentaDto(venta);
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = venta.Id, Action = "Updated", Venta = response.Result }, "ventasQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<VentaDto>> CrearEntidad(VentaDto request)
        {
            var response = new BaseResponse<VentaDto>();
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
                }

                Venta venta = new()
                {
                    ClienteId = request.ClienteId,
                    FechaVenta = DateTime.Now,
                    NumeroFactura = request.NumeroFactura,
                    MetodoPago = request.MetodoPago,
                    Fecha = DateTime.Now,
                    Estado = true
                };

                venta = await _repository.InsertarEntidad(venta);

                response.Result = MapToVentaDto(venta);
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = venta.Id, Action = "Created", Venta = response.Result }, "ventasQueue");
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

                // Publicar evento a RabbitMQ (opcional)
                await _rabbitMQService.PublishMessage(new { Id = id, Action = "Deleted" }, "ventasQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<VentaDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<VentaDto>();
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser un valor positivo.", nameof(id));
                }

                var venta = await _repository.ObtenerEntidad(id);
                if (venta == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = MapToVentaDto(venta);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<List<VentaDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<VentaDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(MapToVentaDto).ToList();

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
    }
}

