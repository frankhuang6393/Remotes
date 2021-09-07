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
    public class UserDAO : BaseDAO<UserModel>, IUserService
    {
        public UserDAO(IConfiguration configruration) : base(configruration)
        {
        }

        public long CreateUser(UserModel model)
        {
            return (long)Excute("CreateUser", model);
        }

        public UserModel GetUser(string name)
        {
            var parm = new UserModel { UserName = name };
            return Query("GetTop1UserByUserName", parm);
        }

        public void UpdateBalance(UserModel model)
        {
            Excute("UpdateUserBalanceByID", model);
        }
    }
}
