using Dapper;
using Microsoft.Extensions.Configuration;
using Remotes.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remotes.Models
{
    public class APILogDAO : ILogService
    {
        private string _connectString;
        public APILogDAO(IConfiguration configruration)
        {
            _connectString = configruration.GetConnectionString("DefaultConnectionString");
        }

        public long CreateAPIReqLog(APILogModel model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.QueryFirst<long>("CreateAPILog", model, commandType: CommandType.StoredProcedure);
            }
        }

        public void CreateAPIRespLog(APILogModel model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                conn.Execute("UpdateAPILogResponseData", model, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<APILogModel> GetAllAPILog()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.Query<APILogModel>("GetAllAPILog", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
