using Abstracciones.Interfaces.Flujo;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private ICategoriaFlujo categoriaFlujo;
        private ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaFlujo CategoriaFlujo, ILogger<CategoriaController> logger)
        {
            categoriaFlujo = CategoriaFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await categoriaFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await categoriaFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
