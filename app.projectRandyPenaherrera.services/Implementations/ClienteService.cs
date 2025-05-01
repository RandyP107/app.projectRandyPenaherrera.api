using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectRandyPenaherrera.common.Dto;
using app.projectRandyPenaherrera.DataAccess.repositories;
using app.projectRandyPenaherrera.Entities.Models;
using app.projectRandyPenaherrera.services.EventMQ;
using app.projectRandyPenaherrera.services.Interfaces;

namespace app.projectRandyPenaherrera.services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IRabbitMQService _rabbitMQService;

        public ClienteService(IClienteRepository repository, IRabbitMQService rabbitMQService)
        {
            _repository = repository;
            _rabbitMQService = rabbitMQService;
        }

        private ClienteDto MapToClienteDto(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                CedulaIdentidad = cliente.CedulaIdentidad,
                FechaNacimiento = cliente.FechaNacimiento
            };
        }

        public async Task<BaseResponse<ClienteDto>> ActualizarEntidad(int id, ClienteDto request)
        {
            var response = new BaseResponse<ClienteDto>();
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

                Cliente cliente = new()
                {
                    Id = id,
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Email = request.Email,
                    FechaNacimiento = request.FechaNacimiento,
                    Fecha = DateTime.Now,
                    CedulaIdentidad = request.CedulaIdentidad
                };

                await _repository.ActualizarEntidad(cliente);

                response.Result = MapToClienteDto(cliente);
                response.Success = true;

                // Publicar mensaje a RabbitMQ
                await _rabbitMQService.PublishMessage(new { Id = cliente.Id, Action = "Updated" }, "clientesQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<ClienteDto>> CrearEntidad(ClienteDto request)
        {
            var response = new BaseResponse<ClienteDto>();
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
                }

                Cliente cliente = new()
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Email = request.Email,
                    FechaNacimiento = request.FechaNacimiento,
                    Fecha = DateTime.Now,
                    CedulaIdentidad = request.CedulaIdentidad
                };

                cliente = await _repository.InsertarEntidad(cliente);

                response.Result = MapToClienteDto(cliente);
                response.Success = true;

                // Publicar mensaje a RabbitMQ
                await _rabbitMQService.PublishMessage(response.Result, "clientesQueue");
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
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<ClienteDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<ClienteDto>();
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser un valor positivo.", nameof(id));
                }

                var cliente = await _repository.ObtenerEntidad(id);
                if (cliente == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = MapToClienteDto(cliente);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<List<ClienteDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<ClienteDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(MapToClienteDto).ToList();

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