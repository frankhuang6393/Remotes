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
    public class UserDAO : IUserService
    {
        private string _connectString;
        public UserDAO(IConfiguration configruration)
        {
            _connectString = configruration.GetConnectionString("DefaultConnectionString");
        }

        public long CreateUser(UserModel model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.QueryFirst<long>("CreateUser", model, commandType: CommandType.StoredProcedure);
            }
        }

        public UserModel GetUser(string name)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                var parm = new UserModel { UserName = name };
                return conn.QueryFirstOrDefault<UserModel>("GetTop1UserByUserName", parm, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateBalance(UserModel model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                conn.Execute("UpdateUserBalanceByID", model, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
