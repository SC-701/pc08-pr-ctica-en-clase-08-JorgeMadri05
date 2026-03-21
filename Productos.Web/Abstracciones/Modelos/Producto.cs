using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad Nombre es requerida")]
        [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El Nombre no permite caracteres especiales")]
        [StringLength(100, ErrorMessage = "El Nombre es demasiado largo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La propiedad Descripcion es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La propiedad Precio es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La propiedad Stock es requerida")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Stock debe ser mayor a 0")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La propiedad CodigoBarras es requerida")]
        [RegularExpression(@"^\S+$", ErrorMessage = "CodigoBarras no puede tener espacios en blanco")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
    }

    public class ProductoPrecioUSD : ProductoResponse
    {
        public decimal PrecioUSD { get; set; }
    }
}
