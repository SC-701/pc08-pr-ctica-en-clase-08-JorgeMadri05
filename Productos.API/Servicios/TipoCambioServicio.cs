using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TipoCambio;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public TipoCambioServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<decimal> ObtenerTipoCambio()
        {
            var urlBase = _configuracion.ObtenerURLBase("BancoCentralCR");
            var token = _configuracion.ObtenerToken("BancoCentralCR", "Token");
            var fecha = DateTime.Now.ToString("yyyy/MM/dd");

            var url = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            var cliente = _httpClient.CreateClient("url");
            cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var respuesta = await cliente.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();

            var resultadoDeserializado =
                JsonSerializer.Deserialize<TipoCambiBCC.Root>(resultado);
            var tipoCambio = resultadoDeserializado.datos[0].indicadores[0].series[0].valorDatoPorPeriodo;
            return (decimal)tipoCambio;
        }

    }
}
