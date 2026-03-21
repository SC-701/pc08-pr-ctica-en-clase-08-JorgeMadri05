using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Productos.Web.Pages.Producto
{
    public class AgregarProductoModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarProductoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; } = default!;  
        [BindProperty]
        public List<SelectListItem> categorias { get; set; } 
        [BindProperty]
        public List<SelectListItem> subCategorias { get; set; } 
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; } 
        [BindProperty]
        public Guid subCategoriaSeleccionada { get; set; } 

        public async Task<ActionResult> OnGet()
        {
            await ObtenerCategorias(); 
            return Page();

        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            producto.IdSubCategoria = subCategoriaSeleccionada;

            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "AgregarProducto"); 
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            var resultadodeserializado = JsonSerializer.Deserialize<List<CategoriaResponse>>
                (resultado, opciones);

            categorias = resultadodeserializado.Select(c => 
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre,
                }
            ).ToList();


        }

        public async Task<List<SubCategoriaResponse>> ObtenerSubCategorias(Guid categoriaId) 
        {
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "ObtenerSubCategoriaPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));
            var respuesta = await cliente.SendAsync(solicitud);
            var contenido = await respuesta.Content.ReadAsStringAsync();
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoriaResponse>>
                    (resultado, opciones); 
            }
            return new List<SubCategoriaResponse>();
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaId) 
        {
            var subCategoria = await ObtenerSubCategorias(categoriaId);
            return new JsonResult(subCategoria);

        }
    }
}