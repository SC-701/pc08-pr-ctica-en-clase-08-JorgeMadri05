using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Productos.Web.Pages.Producto
{
    [Authorize]
    public class DetallesModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public ProductoPrecioUSD producto { get; set; } = default!;

        public DetallesModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [Authorize]
        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "ObtenerProducto");
            var cliente = ObtenerUsuarioConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };

            producto = JsonSerializer.Deserialize<ProductoPrecioUSD>(resultado, opciones);
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
