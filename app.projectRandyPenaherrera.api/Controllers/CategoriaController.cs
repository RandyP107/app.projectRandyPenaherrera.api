using app.projectRandyPenaherrera.common.Request;
using app.projectRandyPenaherrera.services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Azure;

namespace app.projectRandyPenaherrera.api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController:Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }


        [HttpGet]
        public IActionResult GetHelloWorld()
        {
            return Ok("Hola Mundo -- categoria");
        }


        /**
         * API PARA OBTENER TODAS LAS CATEGORIAS
         * */
        [HttpPost("obtenerCategorias")]
        public async Task<IActionResult> ObtenerCategorias()
        {
            var result = await _categoriaService.GetCategoriaLista();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }




        /**
        * API PARA INSERTAR UNA CATEGORIA
        * */
        [HttpPost("insertarCategoria")]
        public async Task<IActionResult> PostCategories([FromBody] CategoriaRequest request)
        {
            var response = await _categoriaService.CrearCategoria(request);
            return Ok(response);
        }



        /**
        * API PARA OBTENER UNA CATEGORIA POR ID
        * */
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerCategoria(int id)
        {
            var response = await _categoriaService.GetCategoria(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }



        /**
         * API PARA ACTUALIZAR UNA CATEGORIA POR ID
         * */
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ActualizarCategories(int id, [FromBody] CategoriaRequest request)
        {
            var result = await _categoriaService.ActualizarCategoria(id, request);
            return Ok(result);
        }


        /**
         * API PARA ELIMINAR UNA CATEGORIA POR ID
         * */
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> EliminarCategories(int id)
        {
            var result = await _categoriaService.EliminarCategoria(id);
            return Ok(result);
        }

    }
}

