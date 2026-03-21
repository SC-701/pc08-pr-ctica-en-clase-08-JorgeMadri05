using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {
        private readonly ISubCategoriaDA _subCategoriaDA;

        public SubCategoriaFlujo(ISubCategoriaDA subcategoriaDA)
        {
            _subCategoriaDA = subcategoriaDA;
        }

        public async Task<IEnumerable<SubCategoriaResponse>> Obtener()
        {
            return await _subCategoriaDA.Obtener();
        }

        public async Task<IEnumerable<SubCategoriaResponse>> Obtener(Guid Id)
        {
            return await _subCategoriaDA.Obtener(Id);
        }
    }
}
