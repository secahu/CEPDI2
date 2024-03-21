using CEPDI.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.Repositories
{
    public class GenericRepository<U>:IGenericRepository<U> where U:class
    {

        public readonly string _connection;
        private readonly string _tableName;
        private readonly string _tableNameV;


        public GenericRepository(string tableName, string tableNameV = "")
        {
            try
            {
                if (!Boolean.Parse(ConfigurationManager.AppSettings.Get("debug")))
                {
                    _connection = ConfigurationManager.ConnectionStrings["connectionProd"].ConnectionString;
                }
                else
                {
                    _connection = ConfigurationManager.ConnectionStrings["connectionTest"].ConnectionString;
                }
            }
            catch
            {
                if (!Boolean.Parse(ConfigurationManager.AppSettings.Get("debug")))
                {
                    _connection = ConfigurationManager.AppSettings.Get("connectionProd");
                }
                else
                {
                    _connection = ConfigurationManager.AppSettings.Get("connectionTest");
                }
            }
            _tableName = tableName;
            _tableNameV = tableNameV;
        }

        public virtual async Task<long> CreateId(U entity)
        {

            string query = GenerateInsertQuery();
            using (var connection = new SqlConnection(_connection))
            {
                var affectedRows = await connection.QueryFirstOrDefaultAsync<long>(query, entity);
                return affectedRows;
            }
        }
        public virtual async Task<bool> Create(U entity)
        {
            string query = GenerateInsertQuery();
            using (var connection = new SqlConnection(_connection))
            {
                try
                {
                    var affectedRows = await connection.ExecuteAsync(query, entity);
                    return affectedRows > 0;
                }
                catch (Exception e)
                {
                    string error = e.ToString();
                }
            }
            return false;
        }

        public virtual async Task<bool> Delete(long id)
        {
            var PrimaryKey = GeneratePrimaryPropertie(GetProperties);
            string query = $"Delete from {_tableName} where {PrimaryKey} = {id}";
            using (var connection = new SqlConnection(_connection))
            {
                var affectedRows = await connection.ExecuteAsync(query);
                return affectedRows > 0;
            }
        }

        public virtual async Task<IEnumerable<U>> GetAll()
        {
            string query = $"SELECT * FROM {_tableName} ";
            if (!string.IsNullOrEmpty(_tableNameV))
            {
                query = $"SELECT * FROM {_tableNameV} ";
            }
            using (var connection = new SqlConnection(_connection))
            {
                var rows = await connection.QueryAsync<U>(query);
                return rows;
            }
        }

        public virtual async Task<U> GetById(long id)
        {
            var PrimaryKey = GeneratePrimaryPropertie(GetProperties);

            string query = $"SELECT * FROM {_tableName} WHERE {PrimaryKey} = {id}";
            if (!string.IsNullOrEmpty(_tableNameV))
            {
                query = $"SELECT * FROM {_tableNameV} WHERE {PrimaryKey} = {id}";
            }
            using (var connection = new SqlConnection(_connection))
            {
                var row = await connection.QueryFirstOrDefaultAsync<U>(query);
                return row;
            }
        }

        public virtual async Task<bool> Update(long id, U entity)
        {
            string query = GenerateUpdateQuery();
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    var affectedRows = await connection.ExecuteAsync(query, entity);
                    return affectedRows > 0;
                }
            }
            catch (Exception e)
            {
                string error = e.ToString();
            }
            return false;
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(U).GetProperties();

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
        private static string GeneratePrimaryPropertie(IEnumerable<PropertyInfo> listOfProperties)
        {
            string prope = string.Empty;
            var elementos=(from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length > 0 && (attributes[0] as DescriptionAttribute)?.Description == "Primary"
                    select prop.Name).ToList();
            if (elementos != null && elementos.Count>0) {
                prope = elementos.First();
            }
            return prope;
        }
        private string GenerateUpdateQuery()
        {
            var PrimaryKey = GeneratePrimaryPropertie(GetProperties);

            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);
            var exclude = new List<string>() { "Id", "FechaCreacion", "Activo", "Mensaje", PrimaryKey };

            properties.ForEach(property =>
            {
                if (!exclude.Contains(property))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append($" WHERE {PrimaryKey}=@{PrimaryKey}");

            return updateQuery.ToString();
        }

        private string GenerateInsertQuery()
        {
            var PrimaryKey=GeneratePrimaryPropertie(GetProperties);

            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => {
                if (!prop.Equals(PrimaryKey) && !prop.Equals("Id") && !prop.Equals("FechaModificacion") && !prop.Equals("Mensaje"))
                    insertQuery.Append($"[{prop}],");
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => {
                if (!prop.Equals(PrimaryKey) && !prop.Equals("Id") && !prop.Equals("FechaModificacion") && !prop.Equals("Mensaje"))
                    insertQuery.Append($"@{prop},");
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");
            insertQuery.Append(";SELECT CAST(SCOPE_IDENTITY() as BIGINT)");

            return insertQuery.ToString();
        }

    }
}