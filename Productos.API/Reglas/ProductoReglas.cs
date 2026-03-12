using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<decimal> ConvertirPrecioUSD(decimal precioCRC)
        {
            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambio();
            return precioCRC / tipoCambio;
        }
    }
}

