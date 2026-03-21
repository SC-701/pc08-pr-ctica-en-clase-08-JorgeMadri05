using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : Controller
    {
        private ISubCategoriaFlujo subcategoriaFlujo;
        private ILogger<SubCategoriaController> _logger;

        public SubCategoriaController(ISubCategoriaFlujo SubCategoriaFlujo, ILogger<SubCategoriaController> logger)
        {
            subcategoriaFlujo = SubCategoriaFlujo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await subcategoriaFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await subcategoriaFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
