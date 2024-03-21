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
    public class MedicamentosRepository:GenericRepository<MedicamentosModel>,IMedicamentosRepository
    {

        private static string _tableName = "Medicamentos";
        private static string _tableNameV = "Medicamentos";
        public MedicamentosRepository() : base(_tableName, _tableNameV)
        {
        }


        public async Task<MedicamentosModel> getByFiltros(MedicamentosModel filtro)
        {
            string where = " WHERE 1=1 ";


            string sql = $"select * " +
                $" from {_tableName}";


            if (filtro.IdMedicamento > 0)
            {
                where += $" and IdMedicamento=@IdMedicamento ";
            }

            if (!string.IsNullOrEmpty(filtro.Nombre))
            {
                where += $" and Nombre=@Nombre ";
            }

            sql += where;
            using (var connection = new SqlConnection(_connection))
            {
                MedicamentosModel result = new MedicamentosModel();
                try
                {
                    result = await connection.QueryFirstOrDefaultAsync<MedicamentosModel>(sql, filtro);
                }
                catch (Exception E)
                {
                    string error = E.Message;
                }

                return result;
            }
        }

        public override async Task<IEnumerable<MedicamentosModel>> GetAll()
        {

            IEnumerable<MedicamentosModel> rows = null;
            string query = $"SELECT tbl.*,FF.Nombre as FormaFarmaceutica FROM {_tableName} tbl inner join formasfarmaceuticas FF on(FF.IdFormaFarmaceutica=tbl.IdFormaFarmaceutica)";
            
            using (var connection = new SqlConnection(_connection))
            {
                rows = await connection.QueryAsync<MedicamentosModel>(query);
                
            }
            return rows;
        }
    }
}