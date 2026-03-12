using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DA.Repositarios
{
    public class RepositarioDapper : IRepositarioDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly SqlConnection _conexionBaseDeDatos;

        public RepositarioDapper(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _conexionBaseDeDatos = new SqlConnection(_configuracion.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerRepositorio()
        {
            return _conexionBaseDeDatos;
        }
    }
}
