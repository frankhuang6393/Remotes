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
    public class APILogDAO : BaseDAO<APILogModel>, ILogService
    {
        public APILogDAO(IConfiguration configruration) : base(configruration)
        {
        }

        public long CreateAPIReqLog(APILogModel model)
        {
            return (long)Excute("CreateAPILog", model);
        }

        public void CreateAPIRespLog(APILogModel model)
        {
            Excute("UpdateAPILogResponseData", model);
        }
        
        public IEnumerable<APILogModel> GetAllAPILog()
        {
            return QueryItems("GetAllAPILog");
        }
    }
}
