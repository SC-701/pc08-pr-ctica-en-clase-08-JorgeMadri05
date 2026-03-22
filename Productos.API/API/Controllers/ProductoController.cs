using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase, IProductoController
    {
        private IProductoFlujo productoFlujo;
        private ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo vehiculoFlujo, ILogger<ProductoController> logger)
        {
            productoFlujo = vehiculoFlujo;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Agregar(ProductoRequest producto)
        {
            var resultado = await productoFlujo.Agregar(producto);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<IActionResult> Editar(Guid Id, ProductoRequest producto)
        {
            var resultado = await productoFlujo.Editar(Id, producto);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await productoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await productoFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await productoFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
