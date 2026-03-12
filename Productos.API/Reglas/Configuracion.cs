using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private IConfiguration _configuracion;

        public Configuracion(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }
        public string? ObtenerURLBase(string seccion)
        {
            return _configuracion.GetSection(seccion).Get<APIEndPoint>().UrlBase;
        }
        public string ObtenerToken(string seccion, string llave)
        {
            return _configuracion.GetSection(seccion)[llave];
        }
    }
}
