using CEPDI.Models;
using CEPDI.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CEPDI.Repositories
{
    public class FormasFarmaceuticasRepository:GenericRepository<FormasFarmaceuticasModel>,IFormasFarmaceuticasRepository
    {

        private static string _tableName = "FormasFarmaceuticas";
        private static string _tableNameV = "FormasFarmaceuticas";
        public FormasFarmaceuticasRepository() : base(_tableName, _tableNameV)
        {
        }


        public async Task<FormasFarmaceuticasModel> getByFiltros(FormasFarmaceuticasModel filtro)
        {
            string where = " WHERE 1=1 ";


            string sql = $"select * " +
                $" from {_tableName}";

            if (filtro.IdFormaFarmaceutica > 0)
            {
                where += $" and IdFormaFarmaceutica=@IdFormaFarmaceutica ";
            }

            if (!string.IsNullOrEmpty(filtro.Nombre))
            {
                where += $" and nombre=@nombre ";
            }

            sql += where;
            using (var connection = new SqlConnection(_connection))
            {
                FormasFarmaceuticasModel result = new FormasFarmaceuticasModel();
                try
                {
                    result = await connection.QueryFirstOrDefaultAsync<FormasFarmaceuticasModel>(sql, filtro);
                }
                catch (Exception E)
                {
                    string error = E.Message;
                }

                return result;
            }
        }
    }
}