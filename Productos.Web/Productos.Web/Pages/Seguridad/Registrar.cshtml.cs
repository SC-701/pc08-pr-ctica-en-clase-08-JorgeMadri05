using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Autorizacion.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;

namespace Productos.Web.Pages.Seguridad
{
    public class RegistrarModel : PageModel
    {
        [BindProperty]
        public Abstracciones.Modelos.Seguridad.Usuario usuario { get; set; } = default!;
        private IConfiguracion _configuracion;

        public RegistrarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var hash = Autenticacion.GenerarHash(usuario.PasswordHash);
            usuario.PasswordHash = Autenticacion.ObtenerHash(hash);

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "Registro");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync<UsuarioBase>(endpoint, usuario);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("../index");
        }
    }
}
