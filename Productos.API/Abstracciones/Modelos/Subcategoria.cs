using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class SubCategoriaBase
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public Guid IdCategoria { get; set; }
    }

    public class SubCategoriaRequest : SubCategoriaBase
    {
    }

    public class SubCategoriaResponse : SubCategoriaBase
    {
        public Guid Id { get; set; }
        public string NombreCategoria { get; set; }
    }
}
