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
    public class UsuariosRepository:GenericRepository<UsuariosModel>,IUsuariosRepository
    {

        private static string _tableName = "Usuarios";
        private static string _tableNameV = "Usuarios";
        public UsuariosRepository() : base(_tableName, _tableNameV)
        {
        }


        public async Task<UsuariosModel> getByFiltros(UsuariosModel filtro)
        {
            string where = " WHERE 1=1 ";


            string sql = $"select * " +
                $" from {_tableName}";

            if (filtro.IdUsuario > 0)
            {
                where += $" and IdUsuario=@IdUsuario ";
            }

            if (!string.IsNullOrEmpty(filtro.Usuario))
            {
                where += $" and usuario=@usuario ";
            }

            sql += where;
            using (var connection = new SqlConnection(_connection))
            {
                UsuariosModel result = new UsuariosModel();
                try
                {
                    result = await connection.QueryFirstOrDefaultAsync<UsuariosModel>(sql, filtro);
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