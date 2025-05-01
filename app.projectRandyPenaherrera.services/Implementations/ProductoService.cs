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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly IRabbitMQService _rabbitMQService;

        public ProductoService(IProductoRepository repository, IRabbitMQService rabbitMQService)
        {
            _repository = repository;
            _rabbitMQService = rabbitMQService;
        }

        private ProductoDto MapToProductoDto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                CategoriaId = producto.CategoriaId,
                PrecioUnitario = producto.PrecioUnitario
            };
        }

        public async Task<BaseResponse<ProductoDto>> ActualizarEntidad(int id, ProductoDto request)
        {
            var response = new BaseResponse<ProductoDto>();
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

                Producto producto = new()
                {
                    Id = id,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    CategoriaId = request.CategoriaId,
                    PrecioUnitario = request.PrecioUnitario,
                    Fecha = DateTime.Now,
                    Estado = true
                };

                await _repository.ActualizarEntidad(producto);

                response.Result = MapToProductoDto(producto);
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = producto.Id, Action = "Updated", Producto = response.Result }, "productosQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<ProductoDto>> CrearEntidad(ProductoDto request)
        {
            var response = new BaseResponse<ProductoDto>();
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
                }

                Producto producto = new()
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    CategoriaId = request.CategoriaId,
                    PrecioUnitario = request.PrecioUnitario,
                    Fecha = DateTime.Now,
                    Estado = true
                };

                producto = await _repository.InsertarEntidad(producto);

                response.Result = MapToProductoDto(producto);
                response.Success = true;

                // Publicar evento a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = producto.Id, Action = "Created", Producto = response.Result }, "productosQueue");
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
                await _rabbitMQService.PublishMessage(new { Id = id, Action = "Deleted" }, "productosQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<ProductoDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<ProductoDto>();
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser un valor positivo.", nameof(id));
                }

                var result = await _repository.ObtenerEntidad(id);
                if (result == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = MapToProductoDto(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<List<ProductoDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<ProductoDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(MapToProductoDto).ToList();

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