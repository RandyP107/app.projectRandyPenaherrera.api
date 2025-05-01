using app.projectRandyPenaherrera.common.Dto;
using app.projectRandyPenaherrera.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.projectRandyPenaherrera.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpPost("obtenerProductos")]
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

        [HttpPost("insertarProducto")]
        public async Task<IActionResult> Insertar([FromBody] ProductoDto request)
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
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoDto request)
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

