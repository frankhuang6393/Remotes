using Dapper;
using Microsoft.Extensions.Configuration;
using Remotes.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Models
{
    public class BaseDAO<T> : IDaoService<T>
    {
        public string _connectString;
        public BaseDAO(IConfiguration configruration)
        {
            _connectString = configruration.GetConnectionString("DefaultConnectionString");
        }

        public object Excute(string procedureName, T model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.QueryFirstOrDefault<long>(procedureName, model, commandType: CommandType.StoredProcedure);
            }
        }

        public T Query(string procedureName, T model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.QueryFirstOrDefault<T>(procedureName, model, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> QueryItems(string procedureName)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.Query<T>(procedureName, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
