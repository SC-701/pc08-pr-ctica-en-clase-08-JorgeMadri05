using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Productos.Web.Pages.Producto
{
    public class EditarProductoModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public EditarProductoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> categorias { get; set; }

        [BindProperty]
        public List<SelectListItem> subCategorias { get; set; }

        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }

        [BindProperty]
        public Guid subCategoriaSeleccionada { get; set; }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategorias();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions 
                { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoResponse>
                    (resultado, opciones);

                
                if (producto != null)
                {
                    categoriaSeleccionada = Guid.Parse(categorias.Where(m => m.Text == producto.Categoria).FirstOrDefault().Value);
                    subCategorias = (await ObtenerSubCategorias(categoriaSeleccionada)).Select(s =>
                        new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.Nombre,
                            Selected = s.NombreCategoria == producto.Categoria
                        }
                    ).ToList();

                    subCategoriaSeleccionada = Guid.Parse(subCategorias.Where(m => m.Text == producto.SubCategoria).FirstOrDefault().Value);
                }
            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("APIEndPoints", "EditarProducto");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, producto.Id), new ProductoRequest
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras,
                IdSubCategoria = subCategoriaSeleccionada
            });
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
            var subCategorias = await ObtenerSubCategorias(categoriaId);
            return new JsonResult(subCategorias);
        }
    }
}
