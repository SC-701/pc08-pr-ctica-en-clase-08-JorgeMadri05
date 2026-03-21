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
    public class SubCategoriaDA : ISubCategoriaDA
    {
        private IRepositarioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public SubCategoriaDA(IRepositarioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        public async Task<IEnumerable<SubCategoriaResponse>> Obtener()
        {
            string query = @"ObtenerSubCategorias";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoriaResponse>(query);
            return resultadoConsulta;
        }

        public async Task<IEnumerable<SubCategoriaResponse>> Obtener(Guid Id)
        {
            string query = @"ObtenerSubCategoriaPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoriaResponse>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }
    }
}
