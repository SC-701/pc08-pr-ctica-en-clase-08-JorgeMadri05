using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ISubCategoriaDA
    {
        Task<IEnumerable<SubCategoriaResponse>> Obtener();
        Task<IEnumerable<SubCategoriaResponse>> Obtener(Guid Id);
    }
}
