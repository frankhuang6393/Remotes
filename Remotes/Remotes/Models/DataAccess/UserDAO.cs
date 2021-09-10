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
        private readonly IDaoService<UserModel> _daoService;
        public UserDAO(IDaoService<UserModel> daoService)
        {
            _daoService = daoService;
        }

        public long CreateUser(UserModel model)
        {
            return (long)(_daoService.Excute("CreateUser", model) ?? -1L);
        }

        public UserModel GetUser(string name)
        {
            var parm = new UserModel { UserName = name };
            return _daoService.Query("GetTop1UserByUserName", parm);
        }

        public void UpdateBalance(UserModel model)
        {
            _daoService.Excute("UpdateUserBalanceByID", model);
        }
    }
}
