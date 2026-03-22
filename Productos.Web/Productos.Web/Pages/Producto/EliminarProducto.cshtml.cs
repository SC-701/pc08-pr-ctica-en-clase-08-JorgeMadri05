using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Productos.Web.Pages.Producto
{
    [Authorize]
    public class EliminarProductoModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarProductoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;

        [Authorize]
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "ObtenerProducto");
            var cliente = ObtenerUsuarioConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true };
            producto = JsonSerializer.Deserialize<ProductoResponse>
                (resultado, opciones);
            return Page();
        }

        [Authorize]
        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == null)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "EliminarProducto");
            var cliente = ObtenerUsuarioConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        private HttpClient ObtenerUsuarioConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Token");
            var cliente = new HttpClient();
            if (tokenClaim != null)
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            return cliente;
        }
    }
}

