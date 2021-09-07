using Remotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Services
{
    public interface IPlayerService
    {
        public void Register(UserModel model);

        public void Bet(OrderModel model);

        public void ChangeBetResult(OrderModel model);
    }
}
