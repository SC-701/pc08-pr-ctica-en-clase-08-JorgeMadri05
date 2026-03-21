using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class CategoriaFlujo : ICategoriaFlujo
    {
        private readonly ICategoriaDA _categoriaDA;

        public CategoriaFlujo(ICategoriaDA categoriaDA)
        {
            _categoriaDA = categoriaDA;
        }

        public async Task<IEnumerable<CategoriaResponse>> Obtener()
        {
            return await _categoriaDA.Obtener();
        }

        public async Task<CategoriaResponse> Obtener(Guid Id)
        {
            return await _categoriaDA.Obtener(Id);
        }
    }
}
