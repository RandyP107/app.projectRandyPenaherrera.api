
using app.projectRandyPenaherrera.common.Dto;
using app.projectRandyPenaherrera.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.projectRandyPenaherrera.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        private readonly IVentaService _service;

        public VentaController(IVentaService service)
        {
            _service = service;
        }


        [HttpPost("obtenerVentas")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var result = await _service.GetEntidadLista();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("insertarVenta")]
        public async Task<IActionResult> Insertar([FromBody] VentaDto request)
        {
            var response = await _service.CrearEntidad(request);
            return Ok(response);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = await _service.GetEntidad(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] VentaDto request)
        {
            var result = await _service.ActualizarEntidad(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarEntidad(id);
            return Ok(result);
        }


    }
}
