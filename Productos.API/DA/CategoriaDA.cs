using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {
        private IRepositarioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CategoriaDA(IRepositarioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<CategoriaResponse>> Obtener()
        {
            string query = @"ObtenerCategorias"; 
            var resultadoConsulta = await _sqlConnection.QueryAsync<CategoriaResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CategoriaResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerCategoriaPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CategoriaResponse>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }
    }
}
