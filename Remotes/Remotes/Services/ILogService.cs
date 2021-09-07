using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Services
{
    public interface ILogService
    {
        public long CreateAPIReqLog(Models.APILogModel model);

        public void CreateAPIRespLog(Models.APILogModel model);

        public IEnumerable<Models.APILogModel> GetAllAPILog();
    }
}
