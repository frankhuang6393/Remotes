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
        private readonly IDaoService<APILogModel> _daoService;
        public APILogDAO(IDaoService<APILogModel> daoService)
        {
            _daoService = daoService;
        }

        public long CreateAPIReqLog(APILogModel model)
        {
            return (long)(_daoService.Excute("CreateAPILog", model) ?? -1L);
        }

        public void CreateAPIRespLog(APILogModel model)
        {
            _daoService.Excute("UpdateAPILogResponseData", model);
        }
        
        public IEnumerable<APILogModel> GetAllAPILog()
        {
            return _daoService.QueryItems("GetAllAPILog");
        }
    }
}
