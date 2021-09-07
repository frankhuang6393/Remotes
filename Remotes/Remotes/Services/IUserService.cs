using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Services
{
    public interface IUserService
    {
        public long CreateUser(Models.UserModel model);
        
        public Models.UserModel GetUser(string name);

        public void UpdateBalance(Models.UserModel model);
    }
}
