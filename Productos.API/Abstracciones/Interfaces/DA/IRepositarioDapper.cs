using Microsoft.Data.SqlClient;

namespace Abstracciones.Interfaces.DA
{
    public interface IRepositarioDapper
    {
        SqlConnection ObtenerRepositorio();
        
    }
}
