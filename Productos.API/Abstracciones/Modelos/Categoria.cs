using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriaBase
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
    }

    public class CategoriaRequest : CategoriaBase
    {
    }

    public class CategoriaResponse : CategoriaBase
    {
        public Guid Id { get; set; }
    }
}